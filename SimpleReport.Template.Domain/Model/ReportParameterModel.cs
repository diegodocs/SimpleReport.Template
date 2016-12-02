using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReport.Template.Domain.Model
{
    public class ReportParameterModel : EntityWithAuditModel
    {
        public string ExternalName { get; set; }
        public string InternalName { get; set; }
        public string DataType { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
    }
}
