using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleReport.Template.Application.Service;
using SimpleReport.Template.Core.Config;
using SimpleReport.Template.Domain.Model;
using SimpleReport.Template.Infrastructure.ParallelTask;
using SimpleReport.Template.Infrastructure.Repository;
using SimpleReport.Template.Infrastructure.Service;
using System;
using System.Linq;

namespace SimpleReport.Template.UnitTest
{
    [TestClass]
    public class ReportGenerationTests
    {
        private string connectionString = @"";

        [TestMethod]
        public void WhenCreateNewReport_Then_ValidateRequiredParameters()
        {
            //arrange
            var reportName = "Mock_Report";
            var storedProcedure = @"sp_GetReportData";            
            var context = new SqlServerRepository(connectionString);

            var report = new ReportModel
            {
                Name = reportName,
                StoredProcedureName = storedProcedure,
                Parameters = new[] {
                                new ReportParameterModel { ExternalName = "Start Date", InternalName ="startDate", DataType="DateTime", IsRequired = true, DefaultValue = "UtcNow() -5d" },
                                new ReportParameterModel { ExternalName = "End Date", InternalName ="EndDate", DataType="DateTime", IsRequired = true, DefaultValue = "UtcNow()" } }
            };

            var repository = new ReportRepository(context);

            //act
            repository.Insert(report);
            var currentDbInstance = repository.GetById(report.Id);

            //assert
            Assert.IsNotNull(currentDbInstance);
            Assert.IsTrue(currentDbInstance.Id > 0);
            Assert.AreEqual(reportName, currentDbInstance.Name);
            Assert.AreEqual(storedProcedure, currentDbInstance.StoredProcedureName);
        }

        [TestMethod]
        public void WhenCreateNewReportProcess_Then_StatusPending()
        {
            //arrange            
            var globalConfig = new GlobalConfiguration { PathToExportExcelFiles = Environment.CurrentDirectory , TimerIntervalInMiliSeconds = 10000};
            var fileHandler = new FileHandler();
            var excelExportService = new DataTableExportToCsvService(fileHandler);
            var repositoryForSql = new SqlServerRepository(connectionString);            
            var repositoryReportProcess = new ReportProcessRepository(repositoryForSql);
            var reportProcessService = new ReportProcessService(repositoryReportProcess, repositoryForSql, excelExportService, globalConfig);

            //act
            var listToProcess = repositoryReportProcess.GetListPendingToStart();

            var threadManager = new ThreadManager<ReportProcessModel>();

            threadManager.ProcessInParallel(10, listToProcess, reportProcessService.ExecuteProcess, TimeSpan.FromMinutes(2));

            var listToProcessSecondCycle = repositoryReportProcess.GetListPendingToStart();

            //assert            
            Assert.IsTrue(listToProcess.Count() > 0);
            Assert.AreEqual(0, listToProcessSecondCycle.Count() );            
        }
    }
}
