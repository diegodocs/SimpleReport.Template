
using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Service
{
    public interface IReportService : IApplicationBaseService<ReportModel>
    {        
        IEnumerable<ReportModel> GetAll();
    }
}