using ProductManagement.Core.Application.Interfaces.Services;
using ProductManagement.Core.Application.ViewModels.Business;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IBusinessService : IGenericService<SaveBusinessViewModel, BusinessViewModel> { }
}
