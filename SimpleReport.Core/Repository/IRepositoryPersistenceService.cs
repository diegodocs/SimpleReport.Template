using SimpleReport.Template.Core.Repository;

namespace SimpleReport.Template.Core.Repository
{
    public interface IRepositoryPersistenceService<TEntity> where TEntity : IEntityBase
    {
        void Insert(TEntity instance);
        void Update(TEntity instance);
        void Delete(TEntity instance);
        void Delete(int id);
    }
}
