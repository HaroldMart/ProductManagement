using ProductManagement.Core.Application.Interfaces.Services.Generics;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IProductService : IAditionalMethodsService<SaveProductViewModel, ProductViewModel> { }
}
