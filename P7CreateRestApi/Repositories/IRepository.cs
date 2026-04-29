namespace Dot.Net.WebApi.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> FindAll();
        Task<T?> FindById(int id);
        Task<T> Add(T entity);
        Task<T?> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}