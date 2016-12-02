using System.Collections.Generic;
using SimpleReport.Template.Application.Contracts.Service;
using SimpleReport.Template.Application.Contracts.Repository;
using SimpleReport.Template.Domain.Model;
using System;
using SimpleReport.Template.Core.Repository;
using System.IO;
using SimpleReport.Template.Core.Config;

namespace SimpleReport.Template.Application.Service
{
    public class ReportProcessService : IReportProcessService
    {
        protected readonly IReportProcessRepository reportProcessRepository;        
        protected readonly IRepositoryForSql repositoryForSql;
        protected readonly IDataTableExportToCsvService datatableExportService;
        protected readonly IGlobalConfiguration globalConfig;
        public ReportProcessService(
            IReportProcessRepository reportProcessRepository, IRepositoryForSql repositoryForSql, IDataTableExportToCsvService datatableExportService, IGlobalConfiguration globalConfig)
        {
            this.reportProcessRepository = reportProcessRepository;
            this.datatableExportService = datatableExportService;
            this.repositoryForSql = repositoryForSql;
            this.globalConfig = globalConfig;
        }

        public void AddMessage(ReportProcessModel instance)
        {
            reportProcessRepository.AddMessage(instance);
        }

        public void Delete(int id)
        {
            reportProcessRepository.Delete(id);
        }

        public void Delete(ReportProcessModel instance)
        {
            reportProcessRepository.Delete(instance);
        }

        public IEnumerable<ReportProcessModel> GetListFiltered(string fileName, string reportName, string username)
        {
            return reportProcessRepository.GetListFiltered(fileName, reportName, username);
        }

        public IEnumerable<ReportProcessModel> GetListPendingToStart()
        {
            return reportProcessRepository.GetListPendingToStart();
        }

        public void Insert(ReportProcessModel instance)
        {
            instance.CreatedAt = DateTime.Now;
            reportProcessRepository.Insert(instance);
        }

        public void GenerateFile(ReportProcessModel instance, string path)
        {
            var command = new RepositoryCommand { CommandName = instance.StoredProcedureName };

            foreach (var parameter in instance.Parameters)
            {
                command.Parameters.Add(parameter.ParameterName, parameter.ParameterValue);
            }

            var table = repositoryForSql.GetDataTable(command);

            datatableExportService.Save(table,  path);
        }

        public void ExecuteProcess(ReportProcessModel instance)
        {
            try
            {
                instance.FileName = $"{instance.ReportId}_{instance.ApplicationUserId}_{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.xlsx";
                var path = Path.Combine(globalConfig.PathToExportExcelFiles, instance.FileName);
                //update status to inProgress
                StartProcess(instance);
                //Load Data using report procedure
                GenerateFile(instance, path);
                //update status to done
                DoneProcess(instance);
            }
            catch (Exception ex)
            {
                //update status to error
                instance.Messages.Add(new ReportProcessMessageModel { Message = ex.Message, StackTrace = ex.ToString() });
                AddMessage(instance);
                ErrorProcess(instance);
            }
        }

        public void StartProcess(ReportProcessModel instance)
        {
            instance.StartAt = DateTime.Now;
            reportProcessRepository.Update(instance);
            //Load Data using report procedure


        }
        public void DoneProcess(ReportProcessModel instance)
        {
            instance.EndAt = DateTime.Now;
            instance.Status = eReportProcessStatus.DoneSucessfully;
            reportProcessRepository.Update(instance);
        }
        public void ErrorProcess(ReportProcessModel instance)
        {
            instance.EndAt = DateTime.Now;
            instance.Status = eReportProcessStatus.Error;
            reportProcessRepository.Update(instance);
        }
        public void Update(ReportProcessModel instance)
        {
            instance.UpdateAt = DateTime.Now;
            reportProcessRepository.Update(instance);
        }
    }
}
