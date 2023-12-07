using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;
using ProductManagement.Infrastructure.Persistence.Repositories.Generics;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : AditionalMethods<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context) { }
    }
}
