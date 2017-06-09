using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NAlex.Selling.BL;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace NAlex.Selling.Service
{
    public partial class SellingService : ServiceBase
    {
        static IOperatorParamsFactory paramFactory = new OperatorParamsFactory(int.MaxValue);
        static string dir = ConfigurationManager.AppSettings["directoryPath"];
        static string parsedDir = ConfigurationManager.AppSettings["parsedDirectory"];
        static string notParsedDir = ConfigurationManager.AppSettings["notParsedDirectory"];
        static string logDir = ConfigurationManager.AppSettings["logDir"];
        static string filePattern = ConfigurationManager.AppSettings["filePattern"];
        static string logFile = Path.Combine(logDir, ConfigurationManager.AppSettings["logFileName"]);

        FileSystemWatcher watcher;

        public SellingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.Source = ServiceName;
            EventLog.Log = "Application";

            EventLog.BeginInit();
            if (!EventLog.SourceExists(EventLog.Source))
            {
                EventLog.CreateEventSource(EventLog.Source, EventLog.Log);
            }
            EventLog.EndInit();

            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                if (!Directory.Exists(parsedDir))
                    Directory.CreateDirectory(parsedDir);
                if (!Directory.Exists(notParsedDir))
                    Directory.CreateDirectory(notParsedDir);
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("Error: " + e.Message);
                throw;
            }

            watcher = new FileSystemWatcher(dir, filePattern);
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.IncludeSubdirectories = false;

            watcher.Created += new FileSystemEventHandler(watcher_Created);

            watcher.EnableRaisingEvents = true;

            Task.Factory.StartNew(ProcessExistingFiles);
        }

        protected override void OnStop()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Created -= watcher_Created;
                watcher.Dispose();
            }
        }

        public void StartInteractive(string[] args)
        {
            OnStart(args);
        }

        public void StopInteractive()
        {
            OnStop();
        }

        protected void watcher_Created(object sender, FileSystemEventArgs e)
        {
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

        protected void ProcessExistingFiles()
        {
            var fileNames = Directory.EnumerateFiles(dir, filePattern, SearchOption.TopDirectoryOnly);
            foreach (string fileName in fileNames)
            {
                FileSystemEventArgs e = new FileSystemEventArgs(WatcherChangeTypes.Created, dir, Path.GetFileName(fileName));
                watcher_Created(null, e);
            }
        }

        
    }
}
