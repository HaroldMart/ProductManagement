using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Business;
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

        public async Task<ICollection<BusinessViewModel>> GetAllViewModel()
        {
            List<BusinessViewModel> list = new();

            var data = await _repository.GetAllAsync();

            foreach (var item in data)
            {
                BusinessViewModel business = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                };

                list.Add(business);
            }

            return list;
        }
        public async Task<ICollection<BusinessViewModel>> GetAllViewModelWithInclude(string[] properties)
        {
            List<BusinessViewModel> list = [];

            var data = await _repository.GetAllWithIncludeAsync(properties);

            foreach (var item in data)
            {
                BusinessViewModel business = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    CategoriesCount = item.Categories.Count
                };

                list.Add(business);
            }

            return list;
        }
        public async Task<BusinessViewModel?> GetViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                BusinessViewModel business = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Image = data.Image,
                };

                return business;
            }

            return null;
        }
        public async Task<BusinessViewModel?> GetViewModelWithInclude(int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
            {
                BusinessViewModel business = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Image = data.Image,
                    CategoriesCount = data.Categories.Count
                };

                return business;
            }

            return null;
        }
        public async Task<SaveBusinessViewModel?> GetSaveViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                SaveBusinessViewModel business = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Image = data.Image,
                    IdUser = data.IdUser,
                };

                return business;
            }

            return null;
        }
        public async Task<string> Add(SaveBusinessViewModel saveViewModel)
        {
            Business business = new()
            {
                Id = saveViewModel.Id,
                Name = saveViewModel.Name,
                Image = saveViewModel.Image,
                IdUser = saveViewModel.IdUser
            };

            var data = await _repository.AddAsync(business);

            if (data != null)
            {
                return "Inserted";
            }

            return "Failed";
        }
        public async Task<string> Update(SaveBusinessViewModel saveViewModel)
        {
            
            Business business = new()
            {
                Id = saveViewModel.Id,
                Name = saveViewModel.Name,
                Image = saveViewModel.Image,
                IdUser = saveViewModel.IdUser,
            };

            bool response = await _repository.UpdateAsync(business);
            if (response)
            {
                return "Updated";
            }

            return "Failed";
        }
        public async Task<string> Delete(int id)
        {
            var data = await _repository.DeleteAsync(id);

            if (data)
            {
                return "Deleted";
            }
            return "Failed";
        }
    }
}
