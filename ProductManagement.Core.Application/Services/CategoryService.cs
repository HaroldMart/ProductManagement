using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Business;
using ProductManagement.Core.Application.ViewModels.Category;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<CategoryViewModel>> GetAllViewModel(string businessId)
        {
            List<CategoryViewModel> list = [];

            try
            {
                var data = await _repository.GetAllAsync();

                list = data.Where(p => p.BusinessId.ToString() == businessId).Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,

                }).ToList();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }
        public async Task<ICollection<CategoryViewModel>> GetAllViewModelWithInclude(string businessId, string[] properties)
        {
            List<CategoryViewModel> list = [];

            try
            {
                var data = await _repository.GetAllWithIncludeAsync(properties);

                list = data.Where(p => p.BusinessId.ToString() == businessId).Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    ProductsCount = c.Products.Count,

                }).ToList();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }
        public async Task<CategoryViewModel?> GetViewModel(string businessId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if (data.BusinessId.ToString() == businessId)
                {
                    CategoryViewModel category = new()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Image = data.Image,
                    };

                    return category;
                }

                return null;
            }

            return null;
        }
        public async Task<CategoryViewModel?> GetViewModelWithInclude(string businessId, int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
            {
                if (data.BusinessId.ToString() == businessId)
                {
                    CategoryViewModel category = new()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Image = data.Image,
                        ProductsCount = data.Products.Count
                    };

                    return category;
                }

                return null;
            }

            return null;
        }
        public async Task<SaveCategoryViewModel?> GetSaveViewModel(string businessId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if (data.BusinessId.ToString() == businessId)
                {
                    SaveCategoryViewModel category = new()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Image = data.Image,
                        BusinessId = data.BusinessId,
                    };

                    return category;
                }

                return null;
            }

            return null;
        }
        public async Task<string> Add(SaveCategoryViewModel saveViewModel)
        {
            if (saveViewModel.BusinessId.ToString() != null)
            {
                Category category = new()
                {
                    Id = saveViewModel.Id,
                    Name = saveViewModel.Name,
                    Image = saveViewModel.Image,
                    BusinessId = saveViewModel.BusinessId
                };

                var data = await _repository.AddAsync(category);

                if (data != null)
                {
                    return "Inserted";
                }

                return "Failed";
            }

            return "Failed";
        }
        public async Task<string> Update(SaveCategoryViewModel saveViewModel)
        {
            if (saveViewModel.BusinessId.ToString() != null)
            {
                Category category = new()
                {
                    Id = saveViewModel.Id,
                    Name = saveViewModel.Name,
                    Image = saveViewModel.Image,
                    BusinessId = saveViewModel.BusinessId
                };

                bool response = await _repository.UpdateAsync(category);
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
