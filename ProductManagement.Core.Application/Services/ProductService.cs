using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Core.Application.ViewModels;

namespace ProductManagement.Core.Application.Services
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<ProductViewModel>> GetAll()
        {
            List<ProductViewModel> list = new();

            var data = await _repository.GetAll();

            foreach (var item in data)
            {
                ProductViewModel product = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Amount = item.Amount,
                    Category = item.Category
                };

                  list.Add(product);
                }

            return list;
        }
        public ProductViewModel Get(int id)
        {
            var data = _repository.Get(id);
            ProductViewModel product = new();
            if (data != null)
            {
                product.Id = data.Id;
                product.Name = data.Name;
                product.Description = data.Description;
                product.Price = data.Price;
                product.Amount = data.Amount;
                product.Category = data.Category;
            }
            return product;
        }
        public bool Save(SaveProductViewModel productObj)
        {
            Product product = new()
            {
                Id = productObj.Id,
                Name = productObj.Name,
                Description = productObj.Description,
                Price = productObj.Price,
                Amount = productObj.Amount,
                CategoryId = productObj.CategoryId
            };

            var data = _repository.Save(product);

            if(data != null)
            {
                return true;
            }

            return false;
        }
        public bool Update(SaveProductViewModel productObj)
        {
            Product product = new()
            {
                Id = productObj.Id,
                Name = productObj.Name,
                Description = productObj.Description,
                Price = productObj.Price,
                Amount = productObj.Amount,
                CategoryId = productObj.CategoryId
            };

            _repository.Update(product);
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var data =  await _repository.Delete(id);
            if(data)
            {
                return true;
            }
            return false;
        }
    }
}
