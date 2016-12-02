using System;

namespace SimpleReport.Template.Domain.Model
{

    public class ApplicationUserModel : EntityWithAuditModel
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public DateTime LastLogin { get; set; }
    }
}