using Omdb.ServicesLibs.Omdb;
using Omdb.ServicesLibs.Omdb.Interfaces;
using Omdb.ServicesLibs.Services;

namespace Omdb.Web.Extensions
{
    public static class ServicesExtensions
    {
        private const string OMDB_API_KEY = "OMDB_API_KEY";

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
    }
}
