namespace SimpleReport.Template.Domain.Model
{

    public class ReportProcessMessageModel : EntityWithAuditModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Type { get; set; }
    }
}