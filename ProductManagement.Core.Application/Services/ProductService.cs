using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<ProductViewModel>> GetAllViewModel(string categoryId)
        {
            List<ProductViewModel> list = [];

            try {
                var data = await _repository.GetAllAsync();

                list = data.Where(p => p.CategoryId.ToString() == categoryId).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Amount = p.Amount,
                    Description = p.Description,
                    Price = p.Price,

                }).ToList();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }
        public async Task<ICollection<ProductViewModel>> GetAllViewModelWithInclude(string categoryId, string[] properties)
        {
            List<ProductViewModel> list = [];

            try
            {
                var data = await _repository.GetAllWithIncludeAsync(properties);

                list = data.Where(p => p.CategoryId.ToString() == categoryId).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Amount = p.Amount,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    CategoryId = p.Category.Id.ToString()

                }).ToList();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }
        public async Task<ProductViewModel?> GetViewModel(string categoryId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if (data.CategoryId.ToString() == categoryId)
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

            return null;
        }
        public async Task<ProductViewModel?> GetViewModelWithInclude(string categoryId, int id, string[] collections, string[] references)
        {
            var data = await _repository.GetWithIncludeAsync(id, collections: collections, references: references);

            if (data != null)
            {
                if (data.CategoryId.ToString() == categoryId)
                {
                    ProductViewModel product = new()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Amount = data.Amount,
                        Description = data.Description,
                        Price = data.Price,
                        CategoryName = data.Category.Name,
                        CategoryId = data.Category.Id.ToString()
                    };

                    return product;
                }

                return null;
            }

            return null;
        }
        public async Task<SaveProductViewModel?> GetSaveViewModel(string categoryId, int id)
        {
            var data = await _repository.GetAsync(id);

            if (data != null)
            {
                if (data.CategoryId.ToString() == categoryId)
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

            return null;
        }
        public async Task<string> Add(SaveProductViewModel saveViewModel)
        {
            if (saveViewModel.CategoryId.ToString() != null)
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

            return "Failed";
        }
        public async Task<string> Update(SaveProductViewModel saveViewModel)
        {
            if (saveViewModel.CategoryId.ToString() != null)
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
