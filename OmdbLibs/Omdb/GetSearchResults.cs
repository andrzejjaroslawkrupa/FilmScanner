using System;
using System.Net.Http;
using System.Threading.Tasks;
using OmdbLibs.Models;
using Newtonsoft.Json;

namespace OmdbLibs.Omdb
{
	public class GetSearchResults : IGetSearchResults
	{
		public async Task<OmdbModel> ReturnSearchResults(string searchCritera)
		{
			const string omdbKey = "";

			using var client = new HttpClient();
			var url = new Uri($"https://www.omdbapi.com/?s={searchCritera}&apikey={omdbKey}");

			var response = await client.GetAsync(url);

			using var content = response.Content;

			var json = await content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<OmdbModel>(json);
		}
	}
}
