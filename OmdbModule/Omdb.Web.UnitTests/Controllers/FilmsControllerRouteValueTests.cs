using Moq;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Services;
using Omdb.Web.Controllers;

namespace Omdb.UnitTests.Controllers
{
    // Kestrel leaves '%2F' encoded in route values; the controller must pass the original text on.
    public class FilmsControllerRouteValueTests
    {
        private readonly Mock<IOmdbService> _omdbServiceMock = new();

        [Theory]
        [InlineData("AC%2FDC", "AC/DC")]
        [InlineData("ac%2fdc", "ac/dc")]
        [InlineData("a%2Fb%2Fc", "a/b/c")]
        [InlineData("fast & furious", "fast & furious")]
        [InlineData("100%", "100%")]
        [InlineData("matrix", "matrix")]
        public async Task Search_RouteValue_PassesOriginalTitleToService(string routeValue, string expected)
        {
            _omdbServiceMock.Setup(m => m.GetSearchResultsBasedOnSearchCritera(It.IsAny<string>(), It.IsAny<int?>()))
                .ReturnsAsync(new SearchResultModel());
            var controller = new FilmsController(_omdbServiceMock.Object);

            await controller.Search(routeValue, 2);

            _omdbServiceMock.Verify(m => m.GetSearchResultsBasedOnSearchCritera(expected, 2), Times.Once);
        }

        [Fact]
        public async Task Film_RouteValueWithEncodedSlash_PassesDecodedIdToService()
        {
            _omdbServiceMock.Setup(m => m.GetFilmBasedOnImdbId(It.IsAny<string>())).ReturnsAsync(new FilmModel());
            var controller = new FilmsController(_omdbServiceMock.Object);

            await controller.Film("a%2Fb");

            _omdbServiceMock.Verify(m => m.GetFilmBasedOnImdbId("a/b"), Times.Once);
        }
    }
}
