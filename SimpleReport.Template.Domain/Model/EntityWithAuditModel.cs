using SimpleReport.Template.Core.Repository;
using System;

namespace SimpleReport.Template.Domain.Model
{
    public class EntityWithAuditModel : IEntityBase
    {
        public int Id { get; set; }
        public string LastUpdateUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Enabled { get; set; }
    }
}
