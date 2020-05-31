using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OmdbServicesLibs.Omdb.Interfaces;
using OmdbServicesLibs.Services;

namespace OmdbServicesLibsTests
{
	public class OmdbServiceTests
	{
		private Mock<IGetSearchResults> _GetSearchResultsMock;
		private Mock<IGetFilmById> _GetFilmByIdMock;

		[SetUp]
		public void Setup()
		{
			_GetSearchResultsMock = new Mock<IGetSearchResults>();
			_GetFilmByIdMock = new Mock<IGetFilmById>();
		}

		[Test]
		public async Task GetSearchResultsBasedOnSearchCritera_ReturnSearchResultsWasUsedOnce()
		{
			var service = new OmdbService(_GetSearchResultsMock.Object, _GetFilmByIdMock.Object);

			await service.GetSearchResultsBasedOnSearchCritera("critera", null);

			_GetSearchResultsMock.Verify(m => m.ReturnSearchResults("critera", null), Times.Once);
		}

		[Test]
		public async Task GetFilmById_ReturnFilmWasUsedOnce()
		{
			var service = new OmdbService(_GetSearchResultsMock.Object, _GetFilmByIdMock.Object);

			await service.GetFilmBasedOnImdbId("id");

			_GetFilmByIdMock.Verify(m => m.ReturnFilm("id"), Times.Once);
		}
	}
}