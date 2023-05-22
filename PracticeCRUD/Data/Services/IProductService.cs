using PracticeCRUD.Data.Base;
using PracticeCRUD.Models;
using System.Linq.Expressions;

namespace PracticeCRUD.Data.Services
{
    public interface IProductService: IEntityBaseRepository<Product>
    {
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdAsync(int id, params Expression<Func<Product, object>>[] includeProperties);
        Task<bool> UpdateAsync(int id, Product entity);
        Task<bool> DeleteAsync(int id);
    }
}
