using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vendinha.BLL;
using Vendinha.BLL.Interfaces;
using Vendinha.Commons.Mapper;
using Vendinha.DAL.Context;
using Vendinha.DAL.Repositories;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.IoC
{
    public static class DependencyInjection
    {
        public static void AddBusinessLogic(IServiceCollection services)
        {
            services.AddScoped<IClientesBLL, ClientesBLL>();
            services.AddScoped<IDividasBLL, DividasBLL>();
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IClientesRepository, ClientesRepository>();
            services.AddScoped<IDividasRepository, DividasRepository>();
        }

        public static void AddContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VendinhaContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
                opt.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);
        }

        public static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new ClienteProfile()));
            services.AddAutoMapper(x => x.AddProfile(new DividaProfile()));
        }
    }
}