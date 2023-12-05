using ProductManagement.Core.Application.ViewModels;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IProductService
    {
        Task<ICollection<ProductViewModel>> GetAll();
        ProductViewModel Get(int id);
        bool Save(SaveProductViewModel entity);
        bool Update(SaveProductViewModel entity);
        Task<bool> Delete(int id);
    }
}
