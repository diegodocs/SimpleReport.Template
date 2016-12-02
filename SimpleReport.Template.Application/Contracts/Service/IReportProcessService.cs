using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Service
{
    public interface IReportProcessService : IApplicationBaseService<ReportProcessModel>
    {
        IEnumerable<ReportProcessModel> GetListPendingToStart();
        IEnumerable<ReportProcessModel> GetListFiltered(string fileName, string reportName, string username);
        void AddMessage(ReportProcessModel instance);
        void StartProcess(ReportProcessModel instance);
        void DoneProcess(ReportProcessModel instance);
        void ErrorProcess(ReportProcessModel instance);
    }
}