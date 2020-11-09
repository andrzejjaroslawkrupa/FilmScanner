using Microsoft.Extensions.DependencyInjection;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;
using OmdbServicesLibs.Services;

namespace FilmScanner.Client.Extensions
{
	public static class ServicesExtensions
	{
		public static void AddOmdbServices(this IServiceCollection services, string apiKey)
		{
			services.AddSingleton<IOmdbService, OmdbService>();
			services.AddSingleton(typeof(IApiKeyProvider),
				new ApiKeyProvider(apiKey));
			services.AddSingleton<IGetSearchResults, GetSearchResults>();
			services.AddSingleton<IGetFilmById, GetFilmById>();
		}
	}
}
