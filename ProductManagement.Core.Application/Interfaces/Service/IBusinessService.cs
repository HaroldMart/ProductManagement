using ProductManagement.Core.Application.ViewModels;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface IBusinessService
    {
        Task<ICollection<BusinessViewModel>> GetAll();
        BusinessViewModel Get(int id);
        bool Save(BusinessViewModel entity);
        bool Update(BusinessViewModel entity);
        Task<bool> Delete(int id);
    }
}
