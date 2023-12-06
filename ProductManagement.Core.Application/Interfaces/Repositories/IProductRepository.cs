using ProductManagement.Core.Application.Interfaces.Repositories;
using ProductManagement.Core.Domain.Entities;
using System.Threading.Tasks;

namespace ProductManagement.Core.Application.Interfaces.Repository
{
    public interface IProductRepository : IGenericRepository<Product> {}
}
