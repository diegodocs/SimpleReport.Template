using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Repository
{
    public interface IReportRepository : 
        IRepositoryPersistenceService<ReportModel>, 
        IRepositoryReadService<ReportModel>
    {
        IEnumerable<ReportModel> GetAll();
    }
}