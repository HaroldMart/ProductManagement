using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.Application.Interfaces.Repositories.Generics;
using ProductManagement.Core.Application.Interfaces.Repository;
using ProductManagement.Infrastructure.Persistence.Contexts;
using ProductManagement.Infrastructure.Persistence.Repositories;
using ProductManagement.Infrastructure.Persistence.Repositories.Generics;

namespace ProductManagement.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {

            #region Contexts
         
           if(configuration.GetValue<bool>("UseInMemoryDatabase"))
           {
               services.AddDbContext<DatabaseContext>(o => o.UseInMemoryDatabase("DatabaseInMemory"));

           } else
           {
               services.AddDbContext<DatabaseContext>(p => p.UseSqlServer(configuration
                   .GetConnectionString("DefaultConnection"), 
                   m => m.MigrationsAssembly(
                       typeof(DatabaseContext).Assembly.FullName)));
           }

            #endregion

            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IAditionalMethodsRepository<>), typeof(AditionalMethods<>));
            services.AddTransient<IBusinessRepository, BusinessRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            
            #endregion
        }
    }
}
