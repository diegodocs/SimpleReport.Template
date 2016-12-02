using System.Collections.Generic;

namespace SimpleReport.Template.Core.Repository
{

    public interface IRepositoryReadService<TEntity>
    {
        IEnumerable<TEntity> GetList(IRepositoryCommand command);
        TEntity GetById(int id);
    }
}