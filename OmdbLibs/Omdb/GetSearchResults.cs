using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OmdbServicesLibs.Models;
using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibs.Omdb
{
	public class GetSearchResults : IGetSearchResults
	{
		private readonly string _ApiKey;

		public GetSearchResults(IApiKeyProvider apiKeyProvider)
		{
			_ApiKey = apiKeyProvider.GetApiKey;
		}

		public async Task<SearchResultModel> ReturnSearchResults(string searchCritera, int? page)
		{
			using var client = new HttpClient();
			var url = new Uri($"https://www.omdbapi.com/?s={searchCritera}&apikey={_ApiKey}&page={page}");

			var response = await client.GetAsync(url);

			using var content = response.Content;

			var json = await content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<SearchResultModel>(json);
		}
	}
}
