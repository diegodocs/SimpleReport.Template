using System.Collections.Generic;
using SimpleReport.Template.Domain.Model;
using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Application.Contracts.Repository;

namespace SimpleReport.Template.Infrastructure.Repository
{
    public class ReportProcessRepository : IReportProcessRepository
    {
        protected readonly IRepositoryCommon context;

        public ReportProcessRepository(IRepositoryCommon context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            context.ExecuteCommand(new RepositoryCommand { CommandName = "spDeleteReportProcess", Parameters = new Dictionary<string, object> { { "ReportProcessId", id } } });
        }

        public void Delete(ReportProcessModel instance)
        {
            Delete(instance.Id);
        }

        public IEnumerable<ReportProcessModel> GetList(IRepositoryCommand command)
        {
            return context.GetList<ReportProcessModel>(command);
        }

        public IEnumerable<ReportProcessModel> GetListPendingToStart()
        {
            return context.GetList<ReportProcessModel>
                (new RepositoryCommand
                {
                    CommandName = "spGetReportProcessPendingToStart",
                    Parameters = new Dictionary<string, object> { }
                });
        }

        public IEnumerable<ReportProcessModel> GetListFiltered(string fileName, string reportName, string username)
        {
            return context.GetList<ReportProcessModel>(new RepositoryCommand
            {
                CommandName = "spGetReportProcessFiltered",
                Parameters = new Dictionary<string, object> {
                    { "FileName", fileName },
                    { "ReportName", reportName },
                    { "UserName", username}
                }
            });
        }

        public ReportProcessModel GetById(int id)
        {
            return context.GetSingle<ReportProcessModel>(
                new RepositoryCommand
                {
                    CommandName = "spGetReportProcessById",
                    Parameters = new Dictionary<string, object> { { "ReportProcessId", id } }
                });
        }

        public void Insert(ReportProcessModel instance)
        {
            instance.Id = context.ExecuteInsert(new RepositoryCommand
            {
                CommandName = "spInsertReportProcess",
                Parameters = new Dictionary<string, object> {
                    { "FileName", instance.FileName },
                    { "ReportId", instance.ReportId },
                    { "ApplicationUserId", instance.ApplicationUserId },
                    { "CreatedAt", instance.CreatedAt },
                    { "Parameters", instance.Parameters }
                }
            });
        }

        public void Update(ReportProcessModel instance)
        {
            context.ExecuteCommand(new RepositoryCommand
            {
                CommandName = "spUpdateReportProcess",
                Parameters = new Dictionary<string, object> {
                    { "FileName", instance.FileName },
                    { "ReportId", instance.ReportId },
                    { "ReportProcessId", instance.Id },
                    { "ApplicationUserId", instance.ApplicationUserId },
                    { "UpdateAt", instance.UpdateAt },
                    { "LastUpdateUserName", instance.LastUpdateUserName },
                    { "Status", instance.Status.GetHashCode() },
                    { "StartAt", instance.StartAt },
                    { "EndAt", instance.EndAt },
                    { "Parameters", instance.Parameters }
                }
            });
        }

        public void AddMessage(ReportProcessModel instance)
        {
            context.ExecuteCommand(new RepositoryCommand
            {
                CommandName = "spInsertReportProcessMessage",
                Parameters = new Dictionary<string, object> {
                    { "ReportProcessId", instance.Id },
                    { "Messages", instance.Messages }
                }
            });
        }
    }
}