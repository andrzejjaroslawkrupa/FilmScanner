using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmScanner.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OmdbServicesLibs.Models;
using OmdbServicesLibs.Services;

namespace FilmScannerApiTests
{
	public class ExternalFilmsControllerFilmTests
	{
		private Mock<IOmdbService> _OmdbServiceMock;

		[SetUp]
		public void Setup()
		{
			_OmdbServiceMock = new Mock<IOmdbService>();
			_OmdbServiceMock.Setup(m => m.GetFilmBasedOnImdbId(It.IsAny<string>()))
				.ReturnsAsync(GetFilmModel());
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectTitle()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Title, Is.EqualTo("filmTitle"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectActors()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Actors, Is.EqualTo("filmActors"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectAwards()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Awards, Is.EqualTo("filmAwards"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectBoxOffice()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.BoxOffice, Is.EqualTo("filmBoxOffice"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectCountry()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Country, Is.EqualTo("filmCountry"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectDirector()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Director, Is.EqualTo("filmDirector"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectDvd()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Dvd, Is.EqualTo("filmDvd"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectGenre()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Genre, Is.EqualTo("filmGenre"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectImdbId()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.ImdbId, Is.EqualTo("filmImdbId"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectImsbRating()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.ImdbRating, Is.EqualTo("filmImdbRating"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectRated()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Rated, Is.EqualTo("filmRated"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectReleased()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Released, Is.EqualTo("filmReleased"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectRuntime()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Runtime, Is.EqualTo("filmRuntime"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectWebsite()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Website, Is.EqualTo("filmWebsite"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectWriter()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Writer, Is.EqualTo("filmWriter"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectImdbVotes()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.ImdbVotes, Is.EqualTo("filmImdbVotes"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectLanguage()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Language, Is.EqualTo("filmLanguage"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectMetascore()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Metascore, Is.EqualTo("filmMetascore"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectPlot()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Plot, Is.EqualTo("filmPlot"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectPoster()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Poster, Is.EqualTo("filmPoster"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectProduction()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Production, Is.EqualTo("filmProduction"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectResponse()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Response, Is.EqualTo("filmResponse"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectType()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Type, Is.EqualTo("filmType"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectYear()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Year, Is.EqualTo("filmYear"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectRatingsSource()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			var ratings = model.Ratings.First();
			Assert.That(ratings, Is.Not.Null);
			Assert.That(ratings.Source, Is.EqualTo("ratingsSource"));
		}

		[Test]
		public async Task Film_Id_FilmModelWithCorrectRatingsValue()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Film("1");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as FilmModel;
			Assert.That(model, Is.Not.Null);
			var ratings = model.Ratings.First();
			Assert.That(ratings, Is.Not.Null);
			Assert.That(ratings.Value, Is.EqualTo("ratingsValue"));
		}

		private FilmModel GetFilmModel()
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