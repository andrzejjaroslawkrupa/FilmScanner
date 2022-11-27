using Microsoft.AspNetCore.Mvc;
using Moq;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Services;
using Omdb.Web.Controllers;

namespace Omdb.Web.UnitTests.Controllers
{
    public class FilmsControllerSearchTests
    {
        private readonly Mock<IOmdbService> _omdbServiceMock;

        public FilmsControllerSearchTests()
        {
            _omdbServiceMock = new Mock<IOmdbService>();
            _omdbServiceMock.Setup(m => m.GetSearchResultsBasedOnSearchCritera(It.IsAny<string>(), null))
                .ReturnsAsync(GetSearchResultModel());
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectTotalResults()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            Assert.Equal("resultTotalResults", model?.TotalResults);
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectResponse()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            Assert.Equal("resultResponse", model?.Response);
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchTitle()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            var responseSearches = model?.Searches;
            Assert.NotNull(responseSearches);
            Assert.Equal("searchTitle", responseSearches?.First().Title);
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchPoster()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            var responseSearches = model?.Searches;
            Assert.NotNull(responseSearches);
            Assert.Equal("searchPoster", responseSearches?.First().Poster);
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchImdbId()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            var responseSearches = model?.Searches;
            Assert.NotNull(responseSearches);
            Assert.Equal("searchImdbId", responseSearches?.First().ImdbID);
        }

        [Fact]
        public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchYear()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Search("critera");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as SearchResultModel;
            Assert.NotNull(model);
            var responseSearches = model?.Searches;
            Assert.NotNull(responseSearches);
            Assert.Equal("searchYear", responseSearches?.First().Year);
        }

        private static SearchResultModel GetSearchResultModel()
        {
            return new SearchResultModel
            {
                Response = "resultResponse",
                TotalResults = "resultTotalResults",
                Searches = new List<Search>()
                {
                    new Search()
                    {
                        ImdbID = "searchImdbId",
                        Poster = "searchPoster",
                        Title = "searchTitle",
                        Year = "searchYear"
                    }
                }
            };
        }
    }
}