using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _dbContext;
        protected DbSet<Product> entrySet
        {
            get
            {
                return _dbContext.Set<Product>();
            }
        }
        public ProductRepository(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await entrySet.AsNoTracking().Include(p => p.Category).ToListAsync();
        }
        public Product Get(int id)
        {
            var data = entrySet.AsNoTracking().Include(p => p.Category).FirstOrDefault(i => i.Id == id);
            
            if(data  != null)
            {
                return data;
            }

            Product product = new();

            return product;
        }
        public async Task<Product> Save(Product product)
        {
             await entrySet.AddAsync(product);
            _dbContext.SaveChanges();
            return product;
        }
        public void Update(Product product)
        {
            entrySet.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await entrySet.FindAsync(id);
            if(entity != null)
            {
                entrySet.Remove(entity);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
