using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
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

        public async Task<ICollection<CategoryViewModel>> GetAllViewModel()
        {
            List<CategoryViewModel> list = [];

            var data = await _repository.GetAllAsync();

            foreach (var item in data)
            {
                CategoryViewModel category = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                };

                list.Add(category);
            }

            return list;
        }
        public async Task<ICollection<CategoryViewModel>> GetAllViewModelWithInclude(string[] properties)
        {
            List<CategoryViewModel> list = [];

            var data = await _repository.GetAllWithIncludeAsync(properties);

            foreach (var item in data)
            {
                CategoryViewModel category = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    ProductsCount = item.Products.Count
                };

                list.Add(category);
            }

            return list;
        }
        public async Task<CategoryViewModel?> GetViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
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
        public async Task<CategoryViewModel?> GetViewModelWithInclude(int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
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
        public async Task<SaveCategoryViewModel?> GetSaveViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
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
        public async Task<string> Add(SaveCategoryViewModel saveViewModel)
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
        public async Task<string> Update(SaveCategoryViewModel saveViewModel)
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
