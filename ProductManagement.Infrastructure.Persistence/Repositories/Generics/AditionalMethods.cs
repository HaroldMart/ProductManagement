using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repositories.Generics;
using ProductManagement.Infrastructure.Persistence.Contexts;


namespace ProductManagement.Infrastructure.Persistence.Repositories.Generics
{
    public class AditionalMethods<Entity> : GenericRepository<Entity>, IGenericRepository<Entity>, IAditionalMethodsRepository<Entity>
        where Entity : class
    {
        public AditionalMethods(DatabaseContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Entity>> GetAllWithIncludeAsync(string[] properties)
        {
            var query = entrySet.AsNoTracking().AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
        public async Task<Entity?> GetWithIncludeAsync(int id, string[] collections, string[] references)
        {
            var data = await entrySet.FindAsync(id);
            if (data != null)
            {
                if(references != null)
                {
                    foreach (var path in references)
                    {
                        entrySet.Entry(data).Reference(path).Load();
                    }
                }
                if(collections != null)
                {
                    foreach (var path in collections)
                    {
                        entrySet.Entry(data).Collection(path).Load();
                    }
                }
               
                return data;
            }

            else return null;
        }
    }
}
