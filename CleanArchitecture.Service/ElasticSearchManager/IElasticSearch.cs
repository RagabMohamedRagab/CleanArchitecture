
namespace CleanArchitecture.Service.ElasticSearch { 
    public interface IElasticSearch<T> where T : class 
    {
        Task<bool> CreateDocument(T t);

        Task<ICollection<T>> Search(string val);

        Task<ICollection<T>> GetAll(int size);

        Task<bool> Update(T t, int id);

        Task<bool> Delete(int id);
    }
}
