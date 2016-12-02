using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Domain.Model;
using System.Collections.Generic;

namespace SimpleReport.Template.Application.Contracts.Repository
{
    public interface IApplicationUserRepository :
        IRepositoryPersistenceService<ApplicationUserModel>,
        IRepositoryReadService<ApplicationUserModel>
    {
        IEnumerable<ApplicationUserModel> GetAll();
        ApplicationUserModel GetByLogin(string login);
        void UpdateLastLogin(string login);
    }
}