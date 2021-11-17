using Detran.Infrastructure.Repository;
using Detran.Shared.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Detran.Api
{
    public static class DependecyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped(typeof(IUnitOfWorkFactory<>), typeof(AulaRemotaUnitOfWorkFactory<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<CredPayTokenService>();
            services.AddHttpClient();

            return services;
        }
    }
}