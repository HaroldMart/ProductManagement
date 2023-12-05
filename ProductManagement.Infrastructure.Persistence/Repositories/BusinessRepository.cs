using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DatabaseContext _dbContext;
        protected DbSet<Business> entrySet
        {
            get
            {
                return _dbContext.Set<Business>();
            }
        }
        public BusinessRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Business>> GetAll()
        {
            return await entrySet.AsNoTracking().Include(p => p.Categories).ToListAsync();
        }
        public Business Get(int id)
        {
            var data = entrySet.AsNoTracking().Include(p => p.Categories).FirstOrDefault(i => i.Id == id);

            if (data != null)
            {
                return data;
            }

            Business business = new();

            return business;
        }
        public async Task<Business> Save(Business business)
        {
            await _dbContext.AddAsync(business);
            _dbContext.SaveChanges();
            return business;
        }
        public void Update(Business business)
        {
            entrySet.Entry(business).State = EntityState.Modified;
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
