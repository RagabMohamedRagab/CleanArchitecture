using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Data.IRpositories.IGenericRepositories
{
    public interface IWriteGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);

        void InsertBulk(List<T> entities);
        Task InsertBulkAsync(List<T> entities);

        EntityEntry<T> Update(T entity);


        Task UpdateBulk(List<T> entities);
        void Delete(T entity);
       

    }
}
