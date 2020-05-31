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
	public class ExternalFilmsControllerSearchTests
	{
		private Mock<IOmdbService> _OmdbServiceMock;

		[SetUp]
		public void Setup()
		{
			_OmdbServiceMock = new Mock<IOmdbService>();
			_OmdbServiceMock.Setup(m => m.GetSearchResultsBasedOnSearchCritera(It.IsAny<string>(), null))
				.ReturnsAsync(GetSearchResultModel());
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectTotalResults()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.TotalResults, Is.EqualTo("resultTotalResults"));
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectResponse()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			Assert.That(model.Response, Is.EqualTo("resultResponse"));
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchTitle()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			var responseSearches = model.Searches;
			Assert.That(responseSearches.First().Title, Is.EqualTo("searchTitle"));
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchPoster()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			var responseSearches = model.Searches;
			Assert.That(responseSearches.First().Poster, Is.EqualTo("searchPoster"));
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchImdbId()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			var responseSearches = model.Searches;
			Assert.That(responseSearches.First().ImdbId, Is.EqualTo("searchImdbId"));
		}

		[Test]
		public async Task Search_SearchCritera_SearchResultModelWithCorrectSearchYear()
		{
			var controller = new ExternalFilmsController(_OmdbServiceMock.Object);

			var result = await controller.Search("critera");

			var objectResult = result as ObjectResult;
			Assert.That(objectResult, Is.Not.Null);
			var model = objectResult.Value as SearchResultModel;
			Assert.That(model, Is.Not.Null);
			var responseSearches = model.Searches;
			Assert.That(responseSearches.First().Year, Is.EqualTo("searchYear"));
		}

		private SearchResultModel GetSearchResultModel()
		{
			return new SearchResultModel
			{
				Response = "resultResponse",
				TotalResults = "resultTotalResults",
				Searches = new List<Search>()
				{
					new Search()
					{
						ImdbId = "searchImdbId",
						Poster = "searchPoster",
						Title = "searchTitle",
						Year = "searchYear"
					}
				}
			};
		}
	}
}