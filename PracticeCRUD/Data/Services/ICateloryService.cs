using PracticeCRUD.Data.Base;
using PracticeCRUD.Models;
using System.Linq.Expressions;

namespace PracticeCRUD.Data.Services
{
    public interface ICateloryService: IEntityBaseRepository<Category>
    {
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] includeProperties);
        Task<bool> UpdateAsync(int id, Category entity);
        Task<bool> DeleteAsync(int id);
    }
}
