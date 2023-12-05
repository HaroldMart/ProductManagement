using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _dbContext;
        protected DbSet<Category> entrySet
        {
            get
            {
                return _dbContext.Set<Category>();
            }
        }
        public CategoryRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await entrySet.AsNoTracking().Include(p => p.Products).ToListAsync();
        }
        public Category Get(int id)
        {
            var data = entrySet.AsNoTracking().Include(p => p.Products).FirstOrDefault(i => i.Id == id);

            if (data != null)
            {
                return data;
            }

            Category category = new();

            return category;
        }
        public async Task<Category> Save(Category category)
        {
            await _dbContext.AddAsync(category);
            _dbContext.SaveChanges();
            return category;
        }
        public void Update(Category category)
        {
            entrySet.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await entrySet.FindAsync(id);
            if (entity != null)
            {
                entrySet.Remove(entity);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
