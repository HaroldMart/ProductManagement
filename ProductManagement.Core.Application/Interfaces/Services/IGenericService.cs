using ProductManagement.Core.Application.ViewModels.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel> 
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<ICollection<ViewModel>> GetAllViewModel();
        Task<ICollection<ViewModel>> GetAllViewModelWithInclude(string[] properties);
        Task<ViewModel?> GetViewModel(int id);
        Task<string> Add(SaveViewModel entity);
        Task<string> Update(SaveViewModel entity);
        Task<string> Delete(int id);
    }
}
