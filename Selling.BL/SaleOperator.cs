using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Selling.BL.Reader;
using NAlex.Selling.DTO.Classes;
using NAlex.Selling.DAL.Repositories;
using NAlex.Selling.DAL.Units;

namespace NAlex.Selling.BL
{
    public static class SaleOperator
    {
        public static bool ReadFileToDatabase(string filePath, IOperatorParamsFactory paramFactory, out string errorMessage)
        {
            errorMessage = "";
            bool res = true;

            using (var unit = paramFactory.CreateUnit())
            {
                Guid sessionId = Guid.NewGuid();

                try
                {
                    using (var reader = paramFactory.CreateReader())
                    {
                        reader.Open(filePath);
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
                    errorMessage = lineEx.Message + Environment.NewLine + "Line:  " + lineEx.Line + Environment.NewLine
                        + "Reason: " + lineEx.InnerException.Message;
                    res = false;
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    res = false;
                }

                if (res)
                {
                    try
                    {
                        unit.CopyTempSalesToSales(sessionId);
                    }
                    catch (Exception e)
                    {
                        res = false;
                        errorMessage = "Data not copied for SessionId " + sessionId + e.Message;
                    }
                }

                if (!res)
                    unit.DeleteTempSales(sessionId);
            }

            return res;
        }

        public static void WriteLog(string filePath, string message)
        {

        }
    }
}
