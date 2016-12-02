using System;
using System.Collections.Generic;
using SimpleReport.Template.Application.Contracts.Repository;
using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Domain.Model;

namespace SimpleReport.Template.Infrastructure.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        protected readonly IRepositoryCommon context;
        public ApplicationUserRepository(IRepositoryCommon context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            context.ExecuteCommand(
                new RepositoryCommand
                {
                    CommandName = "spDeleteReportProcess",
                    Parameters = new Dictionary<string, object> { { "ReportProcessId", id } }
                });
        }

        public void Delete(ApplicationUserModel instance)
        {
            Delete(instance.Id);
        }

        public IEnumerable<ApplicationUserModel> GetAll()
        {
            return context.GetList<ApplicationUserModel>(
               new RepositoryCommand
               {
                   CommandName = "spGetApplicationUserAllEnabled",
                   Parameters = new Dictionary<string, object> { }
               });
        }

        public ApplicationUserModel GetById(int id)
        {
            return context.GetSingle<ApplicationUserModel>(
               new RepositoryCommand
               {
                   CommandName = "spGetApplicationUserById",
                   Parameters = new Dictionary<string, object> { { "ApplicationUserId", id } }
               });
        }

        public ApplicationUserModel GetByLogin(string login)
        {
            return context.GetSingle<ApplicationUserModel>(
               new RepositoryCommand
               {
                   CommandName = "spGetApplicationUserByLogin",
                   Parameters = new Dictionary<string, object> { { "Login", login } }
               });
        }

        public IEnumerable<ApplicationUserModel> GetList(IRepositoryCommand command)
        {
            return context.GetList<ApplicationUserModel>(command);
        }

        public void Insert(ApplicationUserModel instance)
        {
            instance.Id = context.ExecuteInsert(new RepositoryCommand
            {
                CommandName = "spInsertApplicationUser",
                Parameters = new Dictionary<string, object> {
                    { "Name", instance.Name },
                    { "Email", instance.Email },
                    { "Login", instance.Login },
                    { "ApplicationId", instance.ApplicationId },                    
                    { "CreatedAt", instance.CreatedAt }                    
                }
            });
        }

        public void Update(ApplicationUserModel instance)
        {
            context.ExecuteCommand(new RepositoryCommand
            {
                CommandName = "spInsertApplicationUser",
                Parameters = new Dictionary<string, object> {
                    { "Name", instance.Name },
                    { "Email", instance.Email },
                    { "Login", instance.Login },
                    { "ApplicationUserId", instance.Id },
                    { "ApplicationId", instance.ApplicationId },
                    { "CreatedAt", instance.CreatedAt },
                    { "UpdateAt", instance.UpdateAt },
                    { "LastUpdateUserName", instance.LastUpdateUserName }
                }
            });
        }

        public void UpdateLastLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}