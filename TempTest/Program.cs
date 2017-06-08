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
        static string logFile;

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
            logFile = Path.Combine(logDir, "log.txt");
            FileSystemWatcher watcher = new FileSystemWatcher(dir, filePattern);
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.IncludeSubdirectories = false;

            watcher.Created += new FileSystemEventHandler(watcher_Created);
            
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("any key to stop.");

            ProcessExistingFiles();

            Console.ReadKey();

            watcher.Created -= watcher_Created;
            
            watcher.Dispose();
        }

        static void ProcessExistingFiles()
        {
            var fileNames = Directory.EnumerateFiles(dir, filePattern, SearchOption.TopDirectoryOnly);
            foreach (string fileName in fileNames)
            {
                FileSystemEventArgs e = new FileSystemEventArgs(WatcherChangeTypes.Created, dir, Path.GetFileName(fileName));
                watcher_Created(null, e);
            }
        }

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.ChangeType);
            Console.WriteLine(Path.GetFullPath(e.FullPath));
            FileTaskParams par = new FileTaskParams()
            {
                FilePath = Path.GetFullPath(e.FullPath),
                LogFile = logFile,
                ParsedDir = parsedDir,
                NotParsedDir = notParsedDir,
                ParamFactory = paramFactory
            };
     
            Task.Factory.StartNew(SaleOperator.ProcessFile, par);
        }
    }
}
