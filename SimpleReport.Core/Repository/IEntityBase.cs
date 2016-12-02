using System;

namespace SimpleReport.Template.Core.Repository
{
    public interface IEntityBase
    {
        DateTime CreatedAt { get; set; }
        bool Enabled { get; set; }
        int Id { get; set; }
        string LastUpdateUserName { get; set; }
        DateTime UpdateAt { get; set; }
    }
}