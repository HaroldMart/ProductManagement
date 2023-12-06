using ProductManagement.Core.Application.Interfaces.Services;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel> { }
}
