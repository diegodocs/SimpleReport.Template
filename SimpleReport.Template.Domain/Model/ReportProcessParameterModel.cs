namespace SimpleReport.Template.Domain.Model
{

    public class ReportProcessParameterModel : EntityWithAuditModel
    {
        public int ReportParameterId { get; set; }
        public int ReportProcessId { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterName { get; set; }
    }
}