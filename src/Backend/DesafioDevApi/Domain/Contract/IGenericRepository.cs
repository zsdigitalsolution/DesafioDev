using System.Linq.Expressions;

namespace DesafioDevApi.Domain.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> CountAsync();
        Task<TEntity> GetByIdAsync<TKey>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, bool inverse = false);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
