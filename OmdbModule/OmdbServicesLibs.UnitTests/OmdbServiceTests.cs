using Moq;
using NUnit.Framework;
using Omdb.ServicesLibs.Omdb.Interfaces;
using Omdb.ServicesLibs.Services;
using System.Threading.Tasks;

namespace Omdb.ServicesLibs.Tests
{
    public class OmdbServiceTests
    {
        private Mock<IGetSearchResults> _getSearchResultsMock;
        private Mock<IGetFilmById> _getFilmByIdMock;

        [SetUp]
        public void Setup()
        {
            _getSearchResultsMock = new Mock<IGetSearchResults>();
            _getFilmByIdMock = new Mock<IGetFilmById>();
        }

        [Test]
        public async Task GetSearchResultsBasedOnSearchCritera_ReturnSearchResultsWasUsedOnce()
        {
            var service = new OmdbService(_getSearchResultsMock.Object, _getFilmByIdMock.Object);

            await service.GetSearchResultsBasedOnSearchCritera("critera", null);

            _getSearchResultsMock.Verify(m => m.ReturnSearchResults("critera", null), Times.Once);
        }

        [Test]
        public async Task GetFilmById_ReturnFilmWasUsedOnce()
        {
            var service = new OmdbService(_getSearchResultsMock.Object, _getFilmByIdMock.Object);

            await service.GetFilmBasedOnImdbId("id");

            _getFilmByIdMock.Verify(m => m.ReturnFilm("id"), Times.Once);
        }
    }
}