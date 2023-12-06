using ProductManagement.Core.Domain.Entities;
using System.Threading.Tasks;

namespace ProductManagement.Core.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAll();
        Product Get(int id);
        Task<Product> Save(Product entity);
        void Update(Product entity);
        Task<bool> Delete(int id);
    }
}
