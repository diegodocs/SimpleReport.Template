using SimpleReport.Template.Core.Repository;

namespace SimpleReport.Template.Application.Contracts.Service
{
    public interface IApplicationBaseService<TEntity>  where TEntity : IEntityBase
    {
        void Insert(TEntity instance);
        void Update(TEntity instance);
        void Delete(TEntity instance);
        void Delete(int id);
    }
}