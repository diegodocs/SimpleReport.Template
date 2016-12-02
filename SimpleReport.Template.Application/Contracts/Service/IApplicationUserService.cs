using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Service
{
    public interface IApplicationUserService : IApplicationBaseService<ApplicationUserModel>
    {
        IEnumerable<ApplicationUserModel> GetAll();
        ApplicationUserModel GetByLogin(string login);
        void UpdateLastLogin(string login);
    }
}