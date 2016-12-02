using System.Collections.Generic;
using SimpleReport.Template.Core.Repository;

namespace SimpleReport.Template.Core.Repository
{
    public interface IRepositoryCommon
    {
        void ExecuteCommand(IRepositoryCommand commandRepository);
        int ExecuteInsert(IRepositoryCommand commandRepository);        
        IEnumerable<TEntity> GetList<TEntity>(IRepositoryCommand command) where TEntity : new();
        TEntity GetSingle<TEntity>(IRepositoryCommand command) where TEntity : new();
    }
}