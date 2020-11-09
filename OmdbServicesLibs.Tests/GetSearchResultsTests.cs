using Moq;
using NUnit.Framework;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;
using System.Threading.Tasks;

namespace OmdbServicesLibs.Tests
{
	[TestFixture]
	public class GetSearchResultsTests
	{
		private Mock<IApiKeyProvider> _apiKeyProviderMock;

		[SetUp]
		public void Setup()
		{
			_apiKeyProviderMock = new Mock<IApiKeyProvider>();
			_apiKeyProviderMock.Setup(m => m.GetApiKey).Returns("apiKey");
		}

		[Test]
		public async Task ReturnSearchResults_Critera_GetApiKeyUsedOnce()
		{
			var getSearchResults = new GetSearchResults(_apiKeyProviderMock.Object);

			await getSearchResults.ReturnSearchResults("critera", null);

			_apiKeyProviderMock.Verify(m => m.GetApiKey, Times.Once);
		}
	}
}
