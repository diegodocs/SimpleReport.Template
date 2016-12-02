
using SimpleReport.Template.Application.Contracts.Repository;
using SimpleReport.Template.Application.Contracts.Service;
using SimpleReport.Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleReport.Template.Application.Service
{
    public class ReportService : IReportService
    {
        protected readonly IReportRepository reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public void Delete(int id)
        {
            reportRepository.Delete(id);
        }

        public void Delete(ReportModel instance)
        {
            reportRepository.Delete(instance);
        }

        public IEnumerable<ReportModel> GetAll()
        {
            return reportRepository.GetAll();
        }
        
        public void Insert(ReportModel instance)
        {
            instance.CreatedAt = DateTime.Now;
            reportRepository.Insert(instance);
        }

        public void Update(ReportModel instance)
        {
            instance.UpdateAt = DateTime.Now;
            reportRepository.Update(instance);
        }
    }
}
