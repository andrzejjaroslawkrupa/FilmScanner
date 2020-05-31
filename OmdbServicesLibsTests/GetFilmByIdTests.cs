using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using OmdbServicesLibs.Omdb;
using OmdbServicesLibs.Omdb.Interfaces;

namespace OmdbServicesLibsTests
{
	[TestFixture]
	public class GetFilmByIdTests
	{
		private Mock<IApiKeyProvider> _ApiKeyProviderMock;

		[SetUp]
		public void Setup()
		{
			_ApiKeyProviderMock = new Mock<IApiKeyProvider>();
			_ApiKeyProviderMock.Setup(m => m.GetApiKey).Returns("apiKey");
		}

		[Test]
		public async Task ReturnFilm_Id_GetApiKeyUsedOnce()
		{
			var getFilmById = new GetFilmById(_ApiKeyProviderMock.Object);

			await getFilmById.ReturnFilm("id");

			_ApiKeyProviderMock.Verify(m => m.GetApiKey, Times.Once);
		}
	}
}
