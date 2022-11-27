using Microsoft.AspNetCore.Mvc;
using Moq;
using Omdb.ServicesLibs.Models;
using Omdb.ServicesLibs.Services;
using Omdb.Web.Controllers;

namespace Omdb.UnitTests.Controllers
{
    public class FilmsControllerFilmTests
    {
        private readonly Mock<IOmdbService> _omdbServiceMock;

        public FilmsControllerFilmTests()
        {
            _omdbServiceMock = new Mock<IOmdbService>();
            _omdbServiceMock.Setup(m => m.GetFilmBasedOnImdbId(It.IsAny<string>()))
                .ReturnsAsync(GetFilmModel());
        }
        
        [Fact]
        public async Task Film_Id_FilmModelWithCorrectTitle()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmTitle", model?.Title);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectActors()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmActors", model?.Actors);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectAwards()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmAwards", model?.Awards);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectBoxOffice()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmBoxOffice", model?.BoxOffice);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectCountry()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmCountry", model?.Country);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectDirector()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmDirector", model?.Director);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectDvd()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmDvd", model?.Dvd);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectGenre()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmGenre", model?.Genre);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectImdbId()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmImdbId", model?.ImdbId);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectImsbRating()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmImdbRating", model?.ImdbRating);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectRated()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmRated", model?.Rated);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectReleased()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmReleased", model?.Released);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectRuntime()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmRuntime", model?.Runtime);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectWebsite()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmWebsite", model?.Website);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectWriter()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmWriter", model?.Writer);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectImdbVotes()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmImdbVotes", model?.ImdbVotes);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectLanguage()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmLanguage", model?.Language);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectMetascore()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmMetascore", model?.Metascore);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectPlot()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmPlot", model?.Plot);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectPoster()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmPoster", model?.Poster);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectProduction()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmProduction", model?.Production);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectResponse()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmResponse", model?.Response);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectType()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmType", model?.Type);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectYear()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            Assert.Equal("filmYear", model?.Year);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectRatingsSource()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            var ratings = model?.Ratings.First();
            Assert.NotNull(ratings);
            Assert.Equal("ratingsSource", ratings?.Source);
        }

        [Fact]
        public async Task Film_Id_FilmModelWithCorrectRatingsValue()
        {
            var controller = new FilmsController(_omdbServiceMock.Object);

            var result = await controller.Film("1");

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            var model = objectResult?.Value as FilmModel;
            Assert.NotNull(model);
            var ratings = model?.Ratings.First();
            Assert.NotNull(ratings);
            Assert.Equal("ratingsValue", ratings?.Value);
        }

        private static FilmModel GetFilmModel()
        {
            return new FilmModel
            {
                Title = "filmTitle",
                Actors = "filmActors",
                Awards = "filmAwards",
                BoxOffice = "filmBoxOffice",
                Country = "filmCountry",
                Director = "filmDirector",
                Dvd = "filmDvd",
                Genre = "filmGenre",
                ImdbId = "filmImdbId",
                ImdbRating = "filmImdbRating",
                Rated = "filmRated",
                Released = "filmReleased",
                Runtime = "filmRuntime",
                Website = "filmWebsite",
                Writer = "filmWriter",
                ImdbVotes = "filmImdbVotes",
                Language = "filmLanguage",
                Metascore = "filmMetascore",
                Plot = "filmPlot",
                Poster = "filmPoster",
                Production = "filmProduction",
                Response = "filmResponse",
                Type = "filmType",
                Year = "filmYear",
                Ratings = new List<Ratings> { new Ratings
                {
                    Source = "ratingsSource",
                    Value = "ratingsValue"
                } }
            };
        }
    }
}