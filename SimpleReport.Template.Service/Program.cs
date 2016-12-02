using SimpleReport.Template.Application.Service;
using SimpleReport.Template.Core.Config;
using SimpleReport.Template.Domain.Model;
using SimpleReport.Template.Infrastructure.ParallelTask;
using SimpleReport.Template.Infrastructure.Repository;
using SimpleReport.Template.Infrastructure.Service;
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace SimpleReport.Template.Service
{
    public static class Program
    {
        public const string ServiceName = "SimpleReport.Template.Service";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start(args);
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        private static System.Timers.Timer timer;

        private static void Start(string[] args)
        {
            // onstart event
            SetupAndStartTimer();

            Console.WriteLine("Press the Enter key to exit the program.");
            Console.ReadLine();

        }

        private static void SetupAndStartTimer()
        {
            var intervalInMiliseconds = 10000;
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.Interval = intervalInMiliseconds;
            timer.Enabled = true;
            timer.Start();
        }

        private static void TimerElapsed(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Execute();
        }

        private static void Execute()
        {
            var globalConfig = new GlobalConfiguration
            {
                PathToExportExcelFiles = Path.Combine(Environment.CurrentDirectory, "ExportedFiles"),
                TimerIntervalInMiliSeconds = 10000,
                ConnectionString = @"",
                ThreadTimeOutInMiliseconds = 2000
            };
            var fileHandler = new FileHandler();
            var excelExportService = new DataTableExportToCsvService(fileHandler);
            var repositoryForSql = new SqlServerRepository(globalConfig.ConnectionString);
            var repositoryReportProcess = new ReportProcessRepository(repositoryForSql);
            var reportProcessService = new ReportProcessService(repositoryReportProcess, repositoryForSql, excelExportService, globalConfig);

            //act
            var listToProcess = repositoryReportProcess.GetListPendingToStart();

            var threadManager = new ThreadManager<ReportProcessModel>();

            threadManager.ProcessInParallel(10, listToProcess, reportProcessService.ExecuteProcess, TimeSpan.FromMinutes(globalConfig.ThreadTimeOutInMiliseconds));
        }

        private static void Stop()
        {
            // onstop event
            timer.Stop();
            timer.Dispose();
        }
    }
}
