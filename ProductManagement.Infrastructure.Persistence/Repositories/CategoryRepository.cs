using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;
using ProductManagement.Infrastructure.Persistence.Repositories.Generics;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : AditionalMethods<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context) { }
    }
}
