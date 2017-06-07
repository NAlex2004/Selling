using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.DataModel.Entities;
using System.Data.Entity;
using NAlex.Selling.DAL.Units;
using NAlex.Selling.DAL.Repositories;
using System.Data.SqlClient;
using NAlex.Selling.DTO.Classes;
using NAlex.Selling.DAL;
using NAlex.Selling.BL.Reader;
using NAlex.Selling.BL;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace TempTest
{    
    class Program
    {
        static IOperatorParamsFactory paramFactory = new OperatorParamsFactory(int.MaxValue);
        static string dir = ConfigurationManager.AppSettings["directoryPath"];
        static string parsedDir = ConfigurationManager.AppSettings["parsedDirectory"];
        static string notParsedDir = ConfigurationManager.AppSettings["notParsedDirectory"];
        static string logDir = ConfigurationManager.AppSettings["logDir"];
        static string filePattern = ConfigurationManager.AppSettings["filePattern"];

        static void TestReader(ISalesUnit unit)
        {
            Guid session = Guid.NewGuid();
            try
            {
                using (SalesReader reader = new SalesReader())
                {
                    reader.Open("NewManager_18121980.csv");
                    TempSaleDTO tempSale;
                    while ((tempSale = reader.ReadNext()) != null)
                    {
                        tempSale.SessionId = session;
                        unit.TempSales.Add(tempSale);                        
                    }
                }

                unit.SaveChanges();
            }
            catch
            {
                unit.TempSales.Remove(t => t.SessionId.Equals(session));

                unit.SaveChanges();
            }
        }

        static void Main(string[] args)
        {               
            

            FileSystemWatcher watcher = new FileSystemWatcher(dir, filePattern);
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.IncludeSubdirectories = false;

            watcher.Created += new FileSystemEventHandler(watcher_Created);
            
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("any key to stop.");
            Console.ReadKey();

            watcher.Created -= watcher_Created;

            watcher.Dispose();
        }

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(Path.GetFullPath(e.FullPath));

            Task.Factory.StartNew((o) =>
                {
                    string path = (string)o;
                    string error;
                    bool res = SaleOperator.ReadFileToDatabase(path, paramFactory, out error);
                    if (res)
                    {
                        try
                        {
                            File.Copy(path, parsedDir + Path.DirectorySeparatorChar + Path.GetFileName(path), true);
                            File.Delete(path);
                        }
                        catch (Exception ex)
                        {

                        }

                        Console.WriteLine("Parsed:");
                        Console.WriteLine(path);
                        Console.WriteLine();
                    }
                    else
                    {
                        try
                        {
                            File.Copy(path, notParsedDir + Path.DirectorySeparatorChar + Path.GetFileName(path), true);
                            File.Delete(path);
                        }
                        catch (Exception ex)
                        {

                        }

                        Console.WriteLine("Not Parsed:");
                        Console.WriteLine(path);
                        Console.WriteLine(error);
                        Console.WriteLine();
                    }
                }
                , Path.GetFullPath(e.FullPath));
        }
    }
}
