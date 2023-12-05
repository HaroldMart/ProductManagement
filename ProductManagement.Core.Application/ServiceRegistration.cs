using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.Services;

namespace ProductManagement.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {

            #region Services

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBusinessService, BusinessService>();
            
            #endregion
        }
    }
}
