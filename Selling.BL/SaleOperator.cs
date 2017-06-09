using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.BL.Reader;
using NAlex.Selling.DTO.Classes;
using NAlex.Selling.DAL.Repositories;
using NAlex.Selling.DAL.Units;
using System.Threading;
using System.IO;
using System.Configuration;
using NAlex.Selling.DAL;

namespace NAlex.Selling.BL
{
    public struct ReadResult
    {
        public bool HasError;
        public string Message;
        public bool IsIOError;
    }

    public class FileTaskParams
    {
        public IOperatorParamsFactory ParamFactory;
        public string ParsedDir;
        public string NotParsedDir;
        public string LogFile;
    }


    public static class SaleOperator
    {
        static Mutex mutex = new Mutex(false, "LogMutex");

        public static ReadResult ReadToDatabase(IOperatorParamsFactory paramFactory)
        {
            ReadResult res = new ReadResult()
            {
                HasError = false,
                IsIOError = false,
                Message = ""
            };

            using (var unit = paramFactory.CreateUnit())
            {
                Guid sessionId = Guid.NewGuid();

                try
                {
                    using (var reader = paramFactory.CreateReader())
                    {
                        int i = 0;
                        TempSaleDTO sale;

                        while ((sale = reader.ReadNext()) != null)
                        {
                            sale.SessionId = sessionId;
                            unit.TempSales.Add(sale);
                            i++;
                            if (i >= paramFactory.RecordsPerPass)
                                unit.SaveChanges();
                        }

                        unit.SaveChanges();
                    }
                }
                catch (LineParseException lineEx)
                {
                    res.Message = lineEx.Message + Environment.NewLine + "Line:  " + lineEx.Line + Environment.NewLine
                        + "Reason: " + lineEx.InnerException.Message;
                    res.HasError = true;
                }
                catch (Exception e)
                {
                    res.Message = e.Message;
                    res.HasError = true;
                    res.IsIOError = true;
                }

                if (!res.HasError)
                {
                    try
                    {
                        SpResult spRes = unit.CopyTempSalesToSales(sessionId);
                        if (spRes.ErrorNumber != 0)
                        {
                            res.HasError = true;
                            res.Message = "Data not copied for source " + paramFactory.Source + " ."
                                + Environment.NewLine + spRes.ErrorMessage;
                        }
                    }
                    catch (Exception e)
                    {
                        res.HasError = true;
                        res.Message = "Data not copied for source " + paramFactory.Source + ". "
                             + Environment.NewLine + e.Message;
                    }
                }

                if (res.HasError)
                    unit.DeleteTempSales(sessionId);
            }

            return res;
        }

        public static void WriteLog(string filePath, string message)
        {
            mutex.WaitOne();

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("[{0}]: {1}", DateTime.Now, message);
            }

            mutex.ReleaseMutex();
        }

        public static void ProcessFile(object fileTaskParams)
        {
            FileTaskParams taskParams = (FileTaskParams)fileTaskParams;

            ReadResult res = SaleOperator.ReadToDatabase(taskParams.ParamFactory);
            if (!res.HasError)
            {
                try
                {
                    File.Copy(taskParams.ParamFactory.Source, 
                        Path.Combine(taskParams.ParsedDir, Path.GetFileName(taskParams.ParamFactory.Source)), true);
                    File.Delete(taskParams.ParamFactory.Source);
                }
                catch (Exception ex)
                {
                    SaleOperator.WriteLog(taskParams.LogFile, ex.Message);
                }

                SaleOperator.WriteLog(taskParams.LogFile, "Parsed:");
                SaleOperator.WriteLog(taskParams.LogFile, taskParams.ParamFactory.Source);
            }
            else
            {
                SaleOperator.WriteLog(taskParams.LogFile, "NOT PARSED:");
                SaleOperator.WriteLog(taskParams.LogFile, taskParams.ParamFactory.Source);
                SaleOperator.WriteLog(taskParams.LogFile, res.Message);

                if (!res.IsIOError)
                {
                    try
                    {
                        File.Copy(taskParams.ParamFactory.Source,
                            Path.Combine(taskParams.NotParsedDir, Path.GetFileName(taskParams.ParamFactory.Source)), true);
                        File.Delete(taskParams.ParamFactory.Source);
                    }
                    catch (Exception ex)
                    {
                        SaleOperator.WriteLog(taskParams.LogFile, ex.Message);
                    }
                }                
            }

        }
    }
}
