using Moq;
using NUnit.Framework;
using Omdb.ServicesLibs.Omdb;
using Omdb.ServicesLibs.Omdb.Interfaces;
using System.Threading.Tasks;

namespace Omdb.ServicesLibs.Tests
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

        [Test]
        public async Task ReturnSearchResults_NoApiKey_EmptyModel()
        {
            var getSearchResults = new GetSearchResults(_apiKeyProviderMock.Object);

            var result = await getSearchResults.ReturnSearchResults("critera", null);

            Assert.That(result.TotalResults, Is.Null);
            Assert.That(result.Searches, Is.Null);
            Assert.That(result.Response, Is.EqualTo("False"));
        }
    }
}
