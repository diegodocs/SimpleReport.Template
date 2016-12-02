using System.Collections.Generic;
using SimpleReport.Template.Application.Contracts.Service;
using SimpleReport.Template.Domain.Model;
using SimpleReport.Template.Application.Contracts.Repository;
using System;

namespace SimpleReport.Template.Application.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        protected readonly IApplicationUserRepository applicationRepository;
        public ApplicationUserService(IApplicationUserRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public void Delete(int id)
        {
            applicationRepository.Delete(id);
        }

        public void Delete(ApplicationUserModel instance)
        {
            applicationRepository.Delete(instance);
        }

        public IEnumerable<ApplicationUserModel> GetAll()
        {
            return applicationRepository.GetAll();
        }

        public ApplicationUserModel GetByLogin(string login)
        {
            return applicationRepository.GetByLogin(login);
        }

        public void Insert(ApplicationUserModel instance)
        {
            instance.CreatedAt = DateTime.Now;
            applicationRepository.Insert(instance);
        }
        public void Update(ApplicationUserModel instance)
        {
            instance.UpdateAt = DateTime.Now;
            applicationRepository.Update(instance);
        }

        public void UpdateLastLogin(string login)
        {
            applicationRepository.UpdateLastLogin(login);
        }
    }
}
