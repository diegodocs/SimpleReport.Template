using System.Collections.Generic;

namespace SimpleReport.Template.Domain.Model
{
    public class ReportModel : EntityWithAuditModel
    {
        public string Name { get; set; }
        public string StoredProcedureName { get; set; }
        public IEnumerable<ReportParameterModel> Parameters { get; set; }
    }
}
