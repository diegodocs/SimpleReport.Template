using System.Collections.Generic;

namespace SimpleReport.Template.Domain.Model
{

    public class ApplicationModel : EntityWithAuditModel
    {
        public string CurrentVersion { get; set; }
        public string MaxVersion { get; set; }

        public string Name { get; set; }
        public IEnumerable<ApplicationVersionModel> Versions { get; set; }
    }
}