namespace Intaker.TaskManagement.Domain.Services
{
    public interface IRepository<T> : IDisposable 
        where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        
        void Save();        
    }
}
