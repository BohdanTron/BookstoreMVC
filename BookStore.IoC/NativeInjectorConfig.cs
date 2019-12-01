using BookStore.DAL;
using BookStore.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
