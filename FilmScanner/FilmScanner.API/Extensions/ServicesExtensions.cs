using FilmScanner.Contracts;
using FilmScanner.Entities;
using FilmScanner.Repository;
using Microsoft.EntityFrameworkCore;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;
using OmdbServicesLibs.Services;

namespace FilmScanner.API.Extensions
{
    public static class ServicesExtensions
    {
        private const string OMDB_API_KEY = "OMDB_API_KEY";
        private const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";

        public static void AddOmdbServices(this IServiceCollection services)
        {
            var apiKey = Environment.GetEnvironmentVariable(OMDB_API_KEY);

            if (apiKey == null)
                throw new NullReferenceException(OMDB_API_KEY);

            services.AddSingleton<IOmdbService, OmdbService>();
            services.AddSingleton(typeof(IApiKeyProvider),
                new ApiKeyProvider(apiKey));
            services.AddSingleton<IGetSearchResults, GetSearchResults>();
            services.AddSingleton<IGetFilmById, GetFilmById>();
        }

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
