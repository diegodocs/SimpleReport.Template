using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Repository
{
    public interface IReportProcessRepository :
        IRepositoryPersistenceService<ReportProcessModel>,
        IRepositoryReadService<ReportProcessModel>
    {
        IEnumerable<ReportProcessModel> GetListPendingToStart();
        IEnumerable<ReportProcessModel> GetListFiltered(string fileName, string reportName, string username);
        void AddMessage(ReportProcessModel instance);
    }
}