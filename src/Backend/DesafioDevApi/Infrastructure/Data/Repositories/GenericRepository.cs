using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DesafioDevApi.Infrastructure.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApiDBContext _dbContext;

        protected GenericRepository(ApiDBContext context)
        {
            _dbContext = context;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }
        public async Task<TEntity> GetByIdAsync<TKey>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TKey>> orderBy = null, bool inverse = false)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task AddAsync(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
                await _dbContext.Set<TEntity>().AddAsync(item);
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

    }
}
