using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Repository;
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
    }
}
