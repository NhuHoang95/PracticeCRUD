using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using PracticeCRUD.Data.Base;
using PracticeCRUD.Models;
using System.Linq.Expressions;

namespace PracticeCRUD.Data.Services
{
    public class CategoryService : EntityBaseRepository<Category>, ICateloryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Category>().FirstOrDefaultAsync(n => n.CategoryId == id);

            if (entity == null) return false;

            EntityEntry entityEntry = _context.Categories.Entry(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FirstOrDefaultAsync(n => n.CategoryId == id);

        public async Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] includeProperties)
        {
            IQueryable<Category> query = _context.Set<Category>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.CategoryId == id);
        }

        public async Task<bool> UpdateAsync(int id, Category entity)
        {
            var entityDB = await _context.Set<Category>().FirstOrDefaultAsync(n => n.CategoryId == id);
            if (entityDB == null) return false;

            EntityEntry entityEntry = _context.Entry<Category>(entity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
