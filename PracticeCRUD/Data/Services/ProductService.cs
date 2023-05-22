using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PracticeCRUD.Data.Base;
using PracticeCRUD.Models;
using System.Linq.Expressions;

namespace PracticeCRUD.Data.Services
{
    public class ProductService : EntityBaseRepository<Product>, IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Product>().FirstOrDefaultAsync(n => n.ProductId == id);

            if (entity == null) return false;

            EntityEntry entityEntry = _context.Products.Entry(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetByIdAsync(int id) => await _context.Set<Product>().FirstOrDefaultAsync(n => n.ProductId == id);

        public async Task<Product> GetByIdAsync(int id, params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _context.Set<Product>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.ProductId == id);
        }

        public async Task<bool> UpdateAsync(int id, Product entity)
        {
            var entityDB = await _context.Set<Product>().FirstOrDefaultAsync(n => n.ProductId == id);
            if (entityDB == null) return false;

            EntityEntry entityEntry = _context.Entry<Product>(entity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
