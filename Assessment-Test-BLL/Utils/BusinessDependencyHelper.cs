using Assessment_Test_BLL.IService;
using Assessment_Test_BLL.Security;
using Assessment_Test_BLL.Service;
using Assessment_Test_DAL.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment_Test_BLL.Utils
{
    public static class BusinessDependencyHelper
    {
        public static void AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();
        }

        public static void UseBusinessLogicLayer(this IServiceProvider serviceProvider)
        {
            serviceProvider.UseDataAccessLayer();
        }
    }
}
