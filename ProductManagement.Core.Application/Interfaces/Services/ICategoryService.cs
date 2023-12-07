using ProductManagement.Core.Application.Interfaces.Services.Generics;
using ProductManagement.Core.Application.ViewModels.Business;
using ProductManagement.Core.Application.ViewModels.Category;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface ICategoryService : IAditionalMethodsService<SaveCategoryViewModel, CategoryViewModel> { }
}
