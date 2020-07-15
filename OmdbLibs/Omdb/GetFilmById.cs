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
		private readonly string _apiKey;

		public GetFilmById(IApiKeyProvider apiKeyProvider)
		{
			_apiKey = apiKeyProvider.GetApiKey;
		}

		public async Task<FilmModel> ReturnFilm(string imdbId)
		{
			using var client = new HttpClient();
			var url = new Uri($"https://www.omdbapi.com/?i={imdbId}&apikey={_apiKey}");

			var response = await client.GetAsync(url);

			using var content = response.Content;

			var json = await content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<FilmModel>(json);
		}
	}
}