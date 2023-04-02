using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Entities.Models;
using FilmScanner.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmScanner.Web.Extensions
{
    public static class ServicesExtensions
    {
        private const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";

        public static void AddFilmScannerDb(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable(DB_CONNECTION_STRING);

            if (connectionString == null)
                throw new NullReferenceException(DB_CONNECTION_STRING);

            services.AddDbContext<RepositoryContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddDefaultTokenProviders();
            services.AddScoped<UserManager<User>>();
        }
    }
}
