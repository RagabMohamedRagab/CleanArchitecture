using CleanArchitecture.Data.IRpositories.IGenericRepositories;
using CleanArchitecture.Infrastructure.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Infrastructure.Repositories.GenericRepositories
{
    public class WriteGenericRepository<T> : IWriteGenericRepository<T> where T : class
    {
        private readonly SchoolProjectDbContext _dbContext;
        private readonly DbSet<T> _dbset;
        public WriteGenericRepository(SchoolProjectDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
          await _dbset.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void InsertBulk(List<T> entities)
        {
            _dbset.AddRange(entities);
        }

        public async Task InsertBulkAsync(List<T> entities)
        {
           await _dbset.AddRangeAsync(entities);
        }

        public EntityEntry<T> Update(T entity)
        {
           return _dbset.Update(entity);
        }

        public async Task UpdateBulk(List<T> entities)
        {
            _dbset.UpdateRange(entities);
        }

    }
}
