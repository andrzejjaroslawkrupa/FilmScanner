using FilmScanner.Contracts;
using FilmScanner.Controllers;
using FilmScanner.Entities.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmScanner.Tests.Controllers
{
	[TestFixture]
	public class FilmRecordsControllerTests
	{
		private Mock<IRepositoryWrapper> _repositoryMock;
		private FilmRecordsController _controller;
	
		[SetUp]
		public void SetUp()
		{
			_repositoryMock = new Mock<IRepositoryWrapper>();
		}

		[Test]
		public async Task GetAllFilmRecordsForUser_EmptyRepository_ReturnsEmpty()
		{
			var filmRecords = new List<FilmRecord>();
			_repositoryMock.Setup(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(It.IsAny<int>()))
				.ReturnsAsync(filmRecords);
			_controller = new FilmRecordsController(_repositoryMock.Object);

			var result = await _controller.GetAllFilmRecordsForUser(1);

			_repositoryMock.Verify(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(1), Times.Once);
			Assert.That(result, Is.Empty);
		}

		[Test]
		public async Task GetAllFilmRecordsForUser_OneFilmRecordInRepository_ReturnsFilmRecord()
		{
			var filmRecord = GetFilmRecord();
			var filmRecords = new List<FilmRecord>
			{
				filmRecord
			};
			_repositoryMock.Setup(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(It.IsAny<int>()))
				.ReturnsAsync(filmRecords);
			_controller = new FilmRecordsController(_repositoryMock.Object);

			var result = await _controller.GetAllFilmRecordsForUser(1);

			_repositoryMock.Verify(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(1), Times.Once);
			Assert.That(result.First(), Is.EqualTo(filmRecord));
		}

		[Test]
		public async Task GetAllFilmRecordsForUser_MoreThanOneFilmRecordInRepository_ReturnsAllFilmRecords()
		{
			var filmRecord = GetFilmRecord();
			var filmRecords = new List<FilmRecord>
			{
				filmRecord,
				new FilmRecord()
				{
					CreatedAt = DateTime.Now,
					ExternalID = "externalId2",
					ID = 2,
					UserRefID = 1
				}
			};
			_repositoryMock.Setup(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(It.IsAny<int>()))
				.ReturnsAsync(filmRecords);
			_controller = new FilmRecordsController(_repositoryMock.Object);

			var result = await _controller.GetAllFilmRecordsForUser(1);

			_repositoryMock.Verify(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(1), Times.Once);
			CollectionAssert.AreEqual(filmRecords, result);
		}

		private static FilmRecord GetFilmRecord()
		{
			return new FilmRecord()
			{
				ID = 1,
				CreatedAt = DateTime.Now,
				ExternalID = "externalId",
				UserRefID = 1
			};
		}
	}
}