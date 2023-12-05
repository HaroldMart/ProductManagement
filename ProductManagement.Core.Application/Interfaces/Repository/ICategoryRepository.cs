using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAll();
        Category Get(int id);
        Task<Category> Save(Category entity);
        void Update(Category entity);
        Task<bool> Delete(int id);
    }
}
