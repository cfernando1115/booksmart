using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole(Utility.RoleHelper.Admin));
            });
            return services;
        }
    }
}
