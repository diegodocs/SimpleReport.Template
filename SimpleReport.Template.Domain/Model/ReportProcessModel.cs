using System;
using System.Collections.Generic;

namespace SimpleReport.Template.Domain.Model
{
    public class ReportProcessModel : EntityWithAuditModel
    {
        public int ApplicationUserId { get; set; }
        public int ReportId { get; set; }
        public string FileName { get; set; }
        public string StoredProcedureName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public eReportProcessStatus Status { get; set; }
        public IList<ReportProcessParameterModel> Parameters { get; set; }
        public IList<ReportProcessMessageModel> Messages { get; set; }
    }
}