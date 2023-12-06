using ProductManagement.Core.Application.ViewModels;

namespace ProductManagement.Core.Application.Interfaces.Service
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAll();
        CategoryViewModel Get(int id);
        bool Save(CategoryViewModel entity);
        bool Update(CategoryViewModel entity);
        Task<bool> Delete(int id);
    }
}
