namespace Dot.Net.WebApi.Repositories
{
    public interface IRepository<T>
    {
        // Récupérer tous les éléments
        Task<List<T>> FindAll();

        // Récupérer un élément par son Id
        Task<T?> FindById(int id);

        // Créer un nouvel élément
        Task<T> Add(T entity);

        // Mettre à jour un élément existant
        Task<T?> Update(int id, T entity);

        // Supprimer un élément
        Task<bool> Delete(int id);
    }
}