using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseRDMS(connectionString);
            });

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            return services;
        }
        public static DbContextOptionsBuilder UseRDMS(this DbContextOptionsBuilder dbContextBuilder, string connectionString)
        {
            dbContextBuilder.UseNpgsql(connectionString);
            return dbContextBuilder;
        }
    }
}
