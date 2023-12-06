using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<entity> where entity : class
    {
        private readonly DatabaseContext _dbContext;
        protected DbSet<entity> entrySet
        {
            get
            {
                return _dbContext.Set<entity>();
            }
        }
        public GenericRepository(DatabaseContext dbContext)
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
