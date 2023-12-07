using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Application.Interfaces.Repositories.Generics
{
    public interface IAditionalMethodsRepository<Entity>
    {
        Task<ICollection<Entity>> GetAllWithIncludeAsync(string[] properties);
        Task<Entity?> GetWithIncludeAsync(int id, string[] collections, string[] references);
    }
}
