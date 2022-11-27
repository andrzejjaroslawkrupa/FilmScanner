using Omdb.ServicesLibs.Omdb.Interfaces;

namespace Omdb.ServicesLibs.Omdb
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