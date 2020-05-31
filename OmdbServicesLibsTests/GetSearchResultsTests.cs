using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibsTests
{
	[TestFixture]
	public class GetSearchResultsTests
	{
		private Mock<IApiKeyProvider> _ApiKeyProviderMock;

		[SetUp]
		public void Setup()
		{
			_ApiKeyProviderMock = new Mock<IApiKeyProvider>();
			_ApiKeyProviderMock.Setup(m => m.GetApiKey).Returns("apiKey");
		}

		[Test]
		public async Task ReturnSearchResults_Critera_GetApiKeyUsedOnce()
		{
			var getSearchResults = new GetSearchResults(_ApiKeyProviderMock.Object);

			await getSearchResults.ReturnSearchResults("critera", null);

			_ApiKeyProviderMock.Verify(m => m.GetApiKey, Times.Once);
		}
	}
}
