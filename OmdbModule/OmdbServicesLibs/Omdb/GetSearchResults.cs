using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Omdb.Interfaces;

namespace Omdb.ServicesLibs.Omdb
{
    public class GetSearchResults : IGetSearchResults
    {
        private readonly string _apiKey;

        public GetSearchResults(IApiKeyProvider apiKeyProvider)
        {
            _apiKey = apiKeyProvider.GetApiKey;
        }

        public async Task<SearchResultModel> ReturnSearchResults(string searchCritera, int? page)
        {
            using var client = new HttpClient();
            var url = BuildUrl(searchCritera, page, _apiKey);

            var response = await client.GetAsync(url);

            using var content = response.Content;

            var json = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchResultModel>(json);
        }

        // Escape values so characters like '&' or '#' in a title can't break the OMDB query string.
        public static Uri BuildUrl(string searchCritera, int? page, string apiKey) =>
            new Uri($"https://www.omdbapi.com/?s={Uri.EscapeDataString(searchCritera)}&apikey={Uri.EscapeDataString(apiKey ?? string.Empty)}&page={page}");
    }
}
