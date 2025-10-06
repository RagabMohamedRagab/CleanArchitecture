using System.Linq.Expressions;
using CleanArchitecture.Data.IRpositories.IGenericRepositories;
using CleanArchitecture.Infrastructure.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Infrastructure.Repositories.GenericRepositories
{
    public class ReadGenericRepository<T> : IReadGenericRepository<T> where T : class
    {
        private readonly SchoolProjectDbContext _dbContext;
        private readonly DbSet<T> _dbset;
        public ReadGenericRepository(SchoolProjectDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderby)
        {
            IQueryable<T> query = _dbset;
            if (expression != null) {
                query=query.Where(expression);
            }
            if (include != null) {
               query=include(query);
            }
            return query;
           
        }

        public IQueryable GetById(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderby)
        {
            IQueryable<T> query = _dbset;
            if (expression != null) {
                query = query.Where(expression);
            }
            if (include != null) {
                query = include(query);
            }
            return query;
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderby)
        {
            IQueryable<T> query = _dbset;
            if (expression != null) {
                query = query.Where(expression);
            }
            if (include != null) {
                query = include(query);
            }
            return query.AsNoTracking();
        }

        public IQueryable<T> GetManyASTracking(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderby)
        {
            IQueryable<T> query = _dbset;
            if (expression != null) {
                query = query.Where(expression);
            }
            if (include != null) {
                query = include(query);
            }
            return query;
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderby)
        {
            IQueryable<T> query = _dbset;
            if (expression != null) {
                query = query.Where(expression);
            }
            if (include != null) {
                query = include(query);
            }
            return  await query.SingleOrDefaultAsync();
        }
    }
}
