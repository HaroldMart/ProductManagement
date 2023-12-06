using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repositories;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly DatabaseContext _dbContext;

        protected DbSet<Entity> entrySet
        {
            get
            {
                return _dbContext.Set<Entity>();
            }
        }

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Entity>> GetAllAsync()
        {
            return await entrySet.AsNoTracking().ToListAsync();
        }

        public async Task<Entity?> GetAsync(int id)
        {
            var data = await entrySet.FindAsync(id);

            if (data != null)
            {
                return data;
            }

            return null;
        }

        public async Task<ICollection<Entity>> GetAllWithIncludeAsync(string[] properties)
        {
            var query = entrySet.AsNoTracking().AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Entity entity)
        {
            entrySet.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await entrySet.FindAsync(id);
            if (entity != null)
            {
                entrySet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
