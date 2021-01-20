using api_imdb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api_imdb.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Database;Integrated Security=True;");
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddIdentityCore<IdentityUser>()
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddDefaultTokenProviders();

            return services;
        }
    }
}
