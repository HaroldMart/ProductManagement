using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity>
    {
        Task<ICollection<Entity>> GetAllAsync();
        Task<ICollection<Entity>> GetAllWithIncludeAsync(string[] properties);
        Task<Entity?> GetAsync(int id);
        Task<Entity> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(int id);
    }
}
