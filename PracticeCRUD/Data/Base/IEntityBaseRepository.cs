using PracticeCRUD.Models;
using System.Linq.Expressions;

namespace PracticeCRUD.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        //Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        //Task<bool> UpdateAsync(int id, T entity);
        //Task<bool> DeleteAsync(int id);
    }
}
