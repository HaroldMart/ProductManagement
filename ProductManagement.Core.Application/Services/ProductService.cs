using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.Services
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<ProductViewModel>> GetAllViewModel()
        {
            List<ProductViewModel> list = [];

            var data = await _repository.GetAllAsync();

            foreach (var item in data)
            {
                ProductViewModel product = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Description = item.Description,
                    Price = item.Price,
                };

                list.Add(product);
            }

            return list;
        }
        public async Task<ICollection<ProductViewModel>> GetAllViewModelWithInclude(string[] properties)
        {
            List<ProductViewModel> list = [];

            var data = await _repository.GetAllWithIncludeAsync(properties);

            foreach (var item in data)
            {
                ProductViewModel product = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Description = item.Description,
                    Price = item.Price,
                    CategoryName = item.Category.Name
                };

                list.Add(product);
            }

            return list;
        }
        public async Task<ProductViewModel?> GetViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                ProductViewModel product = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Amount = data.Amount,
                    Description = data.Description,
                    Price = data.Price,
                };

                return product;
            }

            return null;
        }
        public async Task<ProductViewModel?> GetViewModelWithInclude(int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
            {
                ProductViewModel product = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Amount = data.Amount,
                    Description = data.Description,
                    Price = data.Price,
                    CategoryName = data.Category.Name
                };

                return product;
            }

            return null;
        }
        public async Task<SaveProductViewModel?> GetSaveViewModel(int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                SaveProductViewModel product = new()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Amount = data.Amount,
                    Description = data.Description,
                    Price = data.Price,
                    CategoryId = data.CategoryId
                };

                return product;
            }

            return null;
        }
        public async Task<string> Add(SaveProductViewModel saveViewModel)
        {
            Product product = new()
            {
                Id = saveViewModel.Id,
                Name = saveViewModel.Name,
                Amount = saveViewModel.Amount,
                Description = saveViewModel.Description,
                Price = saveViewModel.Price,
                CategoryId = saveViewModel.CategoryId,
            };

            var data = await _repository.AddAsync(product);

            if (data != null)
            {
                return "Inserted";
            }

            return "Failed";
        }
        public async Task<string> Update(SaveProductViewModel saveViewModel)
        {

            Product product = new()
            {
                Id = saveViewModel.Id,
                Name = saveViewModel.Name,
                Amount = saveViewModel.Amount,
                Description = saveViewModel.Description,
                Price = saveViewModel.Price,
                CategoryId = saveViewModel.CategoryId,
            };

            bool response = await _repository.UpdateAsync(product);
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
