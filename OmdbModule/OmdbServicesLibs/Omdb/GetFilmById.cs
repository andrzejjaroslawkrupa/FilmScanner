using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Omdb.Interfaces;

namespace Omdb.ServicesLibs.Omdb
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
            var url = BuildUrl(imdbId, _apiKey);

            var response = await client.GetAsync(url);

            using var content = response.Content;

            var json = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FilmModel>(json);
        }

        // Escape values so special characters can't break the OMDB query string.
        public static Uri BuildUrl(string imdbId, string apiKey) =>
            new Uri($"https://www.omdbapi.com/?i={Uri.EscapeDataString(imdbId)}&apikey={Uri.EscapeDataString(apiKey ?? string.Empty)}");
    }
}