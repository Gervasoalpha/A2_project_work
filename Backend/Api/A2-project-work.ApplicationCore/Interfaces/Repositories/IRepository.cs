using A2_project_work.Domain.Entities;

namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : Entity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TPrimaryKey id);

        Task DeleteAll();

        Task<long> Count();
    }
   
}
