namespace SimpleReport.Template.Domain.Model
{

    public class ApplicationVersionModel : EntityWithAuditModel
    {
        public string Version { get; set; }
        public string ScriptFileName { get; set; }                
    }
}