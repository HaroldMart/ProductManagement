using ProductManagement.Core.Application.Interfaces.Repositories.Generics;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Core.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Contexts;
using ProductManagement.Infrastructure.Persistence.Repositories.Generics;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class BusinessRepository : AditionalMethods<Business>, IBusinessRepository
    {
        public BusinessRepository(DatabaseContext context) : base(context) { }
    }
}
