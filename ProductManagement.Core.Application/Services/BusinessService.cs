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

        public async Task<ICollection<BusinessViewModel>> GetAllViewModel(string userId)
        {
            List<BusinessViewModel> list = [];

            try
            {
                var data = await _repository.GetAllAsync();

                var det = data;

                list = data.Where(p => p.IdUser == userId).Select(b => new BusinessViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Image = b.Image,

                }).ToList();
               
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
            return list;
        }
        public async Task<ICollection<BusinessViewModel>> GetAllViewModelWithInclude(string userId, string[] properties)
        {
            List<BusinessViewModel> list = [];

            try
            {
                var data = await _repository.GetAllWithIncludeAsync(properties);

                list = data.Where(p => p.IdUser == userId).Select(b => new BusinessViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Image = b.Image,
                    CategoriesCount = b.Categories.Count

                }).ToList();

             } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }
        public async Task<BusinessViewModel?> GetViewModel(string userId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if(data.IdUser == userId)
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

            return null;
        }
        public async Task<BusinessViewModel?> GetViewModelWithInclude(string userId, int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
            {
                if (data.IdUser == userId)
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

            return null;
        }
        public async Task<SaveBusinessViewModel?> GetSaveViewModel(string userId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if (data.IdUser == userId)
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

            return null;
        }
        public async Task<string> Add(SaveBusinessViewModel saveViewModel)
        {
            if(saveViewModel.IdUser != null)
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

            return "Failed";
        }
        public async Task<string> Update(SaveBusinessViewModel saveViewModel)
        {
            if(saveViewModel.IdUser != null)
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
