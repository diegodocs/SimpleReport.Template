using System;
using System.Collections.Generic;
using SimpleReport.Template.Domain.Model;
using SimpleReport.Template.Core.Repository;
using SimpleReport.Template.Application.Contracts.Repository;

namespace SimpleReport.Template.Infrastructure.Repository
{
    public class ReportRepository : IReportRepository
    {
        protected readonly IRepositoryCommon context;

        public ReportRepository(IRepositoryCommon context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            context.ExecuteCommand(
                new RepositoryCommand
                {
                    CommandName = "spDeleteReport",
                    Parameters = new Dictionary<string, object> { { "ReportId", id } }
                });
        }

        public void Delete(ReportModel instance)
        {
            Delete(instance.Id);
        }

        public IEnumerable<ReportModel> GetList(IRepositoryCommand command)
        {
            return context.GetList<ReportModel>(command);
        }

        public IEnumerable<ReportModel> GetAll()
        {
            return context.GetList<ReportModel>(
                new RepositoryCommand
                {
                    CommandName = "spGetReportAllEnabled",
                    Parameters = new Dictionary<string, object> { }
                });
        }

        public ReportModel GetById(int id)
        {
            return context.GetSingle<ReportModel>(
                new RepositoryCommand
                {
                    CommandName = "spGetReportById",
                    Parameters = new Dictionary<string, object> { { "ReportId", id } }
                });
        }

        public void Insert(ReportModel instance)
        {
            instance.Id = context.ExecuteInsert(new RepositoryCommand
            {
                CommandName = "spInsertReport",
                Parameters = new Dictionary<string, object> {
                    { "Name", instance.Name },
                    { "StoredProcedureName", instance.StoredProcedureName },
                    { "CreatedAt", instance.CreatedAt },
                    { "Parameters", instance.Parameters }
                }
            });
        }

        public void Update(ReportModel instance)
        {
            context.ExecuteCommand(new RepositoryCommand
            {
                CommandName = "spUpdateReport",
                Parameters = new Dictionary<string, object> {
                    { "ReportId", instance.Id },
                    { "Name", instance.Name },
                    { "StoredProcedureName", instance.StoredProcedureName },
                    { "UpdateAt", instance.UpdateAt },
                    { "LastUpdateUserName", instance.LastUpdateUserName },
                    { "Parameters", instance.Parameters }
                }
            });
        }
    }
}