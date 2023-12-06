using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Interfaces.Repository
{
    public interface IBusinessRepository
    {
        Task<ICollection<Business>> GetAll();
        Business Get(int id);
        Task<Business> Save(Business entity);
        void Update(Business entity);
        Task<bool> Delete(int id);
    }
}
