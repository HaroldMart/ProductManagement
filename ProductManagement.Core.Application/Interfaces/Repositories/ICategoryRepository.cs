using ProductManagement.Core.Application.Interfaces.Repositories.Generics;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Interfaces.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>, IAditionalMethodsRepository<Category> { }
}
