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
            var url = new Uri($"https://www.omdbapi.com/?s={searchCritera}&apikey={_apiKey}&page={page}");

            var response = await client.GetAsync(url);

            using var content = response.Content;

            var json = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchResultModel>(json);
        }
    }
}
