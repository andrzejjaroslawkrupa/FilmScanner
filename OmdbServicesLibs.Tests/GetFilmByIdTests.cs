using Moq;
using NUnit.Framework;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;
using System.Threading.Tasks;

namespace OmdbServicesLibs.Tests
{
	[TestFixture]
	public class GetFilmByIdTests
	{
		private Mock<IApiKeyProvider> _apiKeyProviderMock;

		[SetUp]
		public void Setup()
		{
			_apiKeyProviderMock = new Mock<IApiKeyProvider>();
			_apiKeyProviderMock.Setup(m => m.GetApiKey).Returns("apiKey");
		}

		[Test]
		public async Task ReturnFilm_Id_GetApiKeyUsedOnce()
		{
			var getFilmById = new GetFilmById(_apiKeyProviderMock.Object);

			await getFilmById.ReturnFilm("id");

			_apiKeyProviderMock.Verify(m => m.GetApiKey, Times.Once);
		}
	}
}
