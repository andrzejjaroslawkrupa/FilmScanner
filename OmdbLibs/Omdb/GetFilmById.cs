using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OmdbServicesLibs.Models;
using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibs.Omdb
{
	public class GetFilmById : IGetFilmById
	{
		private readonly string _ApiKey;

		public GetFilmById(IApiKeyProvider apiKeyProvider)
		{
			_ApiKey = apiKeyProvider.GetApiKey;
		}

		public async Task<FilmModel> ReturnFilm(string imdbId)
		{
			using var client = new HttpClient();
			var url = new Uri($"https://www.omdbapi.com/?i={imdbId}&apikey={_ApiKey}");

			var response = await client.GetAsync(url);

			using var content = response.Content;

			var json = await content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<FilmModel>(json);
		}
	}
}