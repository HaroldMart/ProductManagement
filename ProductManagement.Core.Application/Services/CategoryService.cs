using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<CategoryViewModel>> GetAll()
        {
            List<CategoryViewModel> list = new();

            var data = await _repository.GetAll();

            foreach (var item in data)
            {
                CategoryViewModel category = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Products = item.Products
                };

                list.Add(category);
            }

            return list;
        }
        public CategoryViewModel Get(int id)
        {
            var data = _repository.Get(id);
            CategoryViewModel category = new();

            if (data != null)
            {
                category.Id = data.Id;
                category.Name = data.Name;
                category.Products = data.Products;
            }
            return category;
        }
        public bool Save(CategoryViewModel categoryObj)
        {
            Category category = new()
            {
                Id = categoryObj.Id,
                Name = categoryObj.Name,
                Products = categoryObj.Products
            };

            var data = _repository.Save(category);

            if (data != null)
            {
                return true;
            }

            return false;
        }
        public bool Update(CategoryViewModel categoryObj)
        {
            Category category = new()
            {
                Id = categoryObj.Id,
                Name = categoryObj.Name,
                Products = categoryObj.Products
            };

            _repository.Update(category);
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
