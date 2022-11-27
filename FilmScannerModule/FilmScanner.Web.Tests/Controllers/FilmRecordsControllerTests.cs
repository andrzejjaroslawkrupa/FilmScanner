using FilmScanner.Contracts;
using FilmScanner.Web.Controllers;
using FilmScanner.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmScanner.Web.UnitTests.Controllers
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
                new FilmRecord
                {
                    CreatedAt = DateTime.Now,
                    ExternalID = "externalId2",
                    ID = 2,
                    UserId = 1
                }
            };
            _repositoryMock.Setup(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(It.IsAny<int>()))
                .ReturnsAsync(filmRecords);
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.GetAllFilmRecordsForUser(1);

            _repositoryMock.Verify(r => r.FilmRecord.GetAllFilmRecordsForUserAsync(1), Times.Once);
            CollectionAssert.AreEqual(filmRecords, result);
        }

        [Test]
        public async Task GetFilmRecordForUser_UserDoesNotExist_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>()));
            _repositoryMock.Setup(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.GetFilmRecordForUser(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(1, 1), Times.Never);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetFilmRecordForUser_FilmRecordDoesNotExist_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.GetFilmRecordForUser(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(1, 1), Times.Once);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetFilmRecordForUser_UserAndFilmRecordExist_CorrectFilmRecord()
        {
            var filmRecord = GetFilmRecord();
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(filmRecord);
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.GetFilmRecordForUser(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(1, 1), Times.Once);
            Assert.That(result.Value, Is.EqualTo(filmRecord));
        }

        [Test]
        public async Task CreateFilmRecord_UserDoesNotExist_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.CreateFilmRecord(1, new FilmRecord());

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Create(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task CreateFilmRecord_NullFilmRecord_BadRequestResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.Create(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.CreateFilmRecord(1, null);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(It.IsAny<int>()), Times.Never);
            _repositoryMock.Verify(r => r.FilmRecord.Create(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task CreateFilmRecord_CorrectFilmRecord_CreatedAtActionResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.Create(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.CreateFilmRecord(1, new FilmRecord());

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Create(It.IsAny<FilmRecord>()), Times.Once);
            Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task UpdateFilmRecord_UserDoesNotExit_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>()));
            _repositoryMock.Setup(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.UpdateFilmRecord(1, 1, GetFilmRecord());

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdateFilmRecord_NullFilmRecord_BadRequestResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.UpdateFilmRecord(1, 1, null);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task UpdateFilmRecord_FilmRecordWithWrongId_BadRequestResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.UpdateFilmRecord(1, 2, GetFilmRecord());

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task UpdateFilmRecord_CorrectFilmRecord_NoContentResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.UpdateFilmRecord(1, 1, GetFilmRecord());

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Update(It.IsAny<FilmRecord>()), Times.Once);
            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteFilmRecord_UserDoesNotExist_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.DeleteFilmRecord(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _repositoryMock.Verify(r => r.FilmRecord.Delete(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteFilmRecord_FilmRecordDoesNotExist_NotFoundResult()
        {
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((FilmRecord)null);
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.DeleteFilmRecord(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(1, 1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Delete(It.IsAny<FilmRecord>()), Times.Never);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task DeleteFilmRecord_UserAndFilmRecordExist_FilmRecordDeleted()
        {
            var filmRecord = GetFilmRecord();
            _repositoryMock.Setup(r => r.User.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());
            _repositoryMock.Setup(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(filmRecord);
            _repositoryMock.Setup(r => r.FilmRecord.Delete(It.IsAny<FilmRecord>()));
            _controller = new FilmRecordsController(_repositoryMock.Object);

            var result = await _controller.DeleteFilmRecord(1, 1);

            _repositoryMock.Verify(r => r.User.GetUserByIdAsync(1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.GetFilmRecordForUserByIdAsync(1, 1), Times.Once);
            _repositoryMock.Verify(r => r.FilmRecord.Delete(filmRecord), Times.Once);
            Assert.That(result.Value, Is.EqualTo(filmRecord));
        }

        private static FilmRecord GetFilmRecord()
        {
            return new FilmRecord
            {
                ID = 1,
                CreatedAt = DateTime.Now,
                ExternalID = "externalId",
                UserId = 1
            };
        }
    }
}