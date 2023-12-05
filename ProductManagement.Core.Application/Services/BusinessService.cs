using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _repository;
        public BusinessService(IBusinessRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<BusinessViewModel>> GetAll()
        {
            List<BusinessViewModel> list = new();

            var data = await _repository.GetAll();

            foreach (var item in data)
            {
                BusinessViewModel business = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Categories = item.Categories
                };

                list.Add(business);
            }

            return list;
        }
        public BusinessViewModel Get(int id)
        {
            var data = _repository.Get(id);
            BusinessViewModel business = new();

            if (data != null)
            {
                business.Id = data.Id;
                business.Name = data.Name;
                business.Categories = data.Categories;
            }
            return business;
        }
        public bool Save(BusinessViewModel businessObj)
        {
            Business business = new()
            {
                Id = businessObj.Id,
                Name = businessObj.Name,
                Categories = businessObj.Categories
            };

            var data = _repository.Save(business);

            if (data != null)
            {
                return true;
            }

            return false;
        }
        public bool Update(BusinessViewModel businessObj)
        {
            Business business = new()
            {
                Id = businessObj.Id,
                Name = businessObj.Name,
                Categories = businessObj.Categories
            };

            _repository.Update(business);
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var data = await _repository.Delete(id);

            if (data)
            {
                return true;
            }
            return false;
        }
    }
}
