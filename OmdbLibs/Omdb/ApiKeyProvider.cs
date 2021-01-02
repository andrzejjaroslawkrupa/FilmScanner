using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibs.Omdb
{
	public class ApiKeyProvider : IApiKeyProvider
	{
		public ApiKeyProvider(string apiKey)
		{
			GetApiKey = apiKey;
		}

		public string GetApiKey { get; }
	}
}