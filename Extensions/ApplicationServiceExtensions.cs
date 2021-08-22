using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSmart.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IMemberService, MemberService>();

            services.AddScoped<IShipmentService, ShipmentService>();

            return services;
        }
    }
}
