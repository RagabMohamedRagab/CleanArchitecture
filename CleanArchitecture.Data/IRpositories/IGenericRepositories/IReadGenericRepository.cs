using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Data.IRpositories.IGenericRepositories
{
    public interface IReadGenericRepository<T> where T : class
    {
       public IQueryable<T> GetManyASTracking(Expression<Func<T, bool>> expression,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderby);
        public IQueryable<T> GetAll(
                 Expression<Func<T, bool>> expression,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include ,
                 Func<IQueryable<T>,IOrderedQueryable<T>> orderby
            );

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> expression,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderby);

        public Task<T> GetSingle(Expression<Func<T, bool>> expression,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderby);


        public IQueryable GetById(Expression<Func<T, bool>> expression,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderby);


    }
}
