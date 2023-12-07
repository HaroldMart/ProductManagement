using ProductManagement.Core.Application.Interfaces.Services.Generics;
using ProductManagement.Core.Application.ViewModels.Business;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IBusinessService : IAditionalMethodsService<SaveBusinessViewModel, BusinessViewModel> { }
}
