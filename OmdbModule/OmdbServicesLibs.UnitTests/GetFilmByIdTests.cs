using Moq;
using NUnit.Framework;
using Omdb.ServicesLibs.Omdb;
using Omdb.ServicesLibs.Omdb.Interfaces;
using System.Threading.Tasks;

namespace Omdb.ServicesLibs.Tests
{
    [TestFixture]
    public class GetFilmByIdTests
    {
        private Mock<IApiKeyProvider> _apiKeyProviderMock;

        [SetUp]
        public void Setup()
        {
            _apiKeyProviderMock = new Mock<IApiKeyProvider>();
            _apiKeyProviderMock.Setup(m => m.GetApiKey).Returns("apiKey");
        }

        [Test]
        public async Task ReturnFilm_Id_GetApiKeyUsedOnce()
        {
            var getFilmById = new GetFilmById(_apiKeyProviderMock.Object);

            await getFilmById.ReturnFilm("id");

            _apiKeyProviderMock.Verify(m => m.GetApiKey, Times.Once);
        }

        [Test]
        public async Task ReturnFilm_NoApiKey_EmptyModel()
        {
            var getFilmById = new GetFilmById(_apiKeyProviderMock.Object);

            var result = await getFilmById.ReturnFilm("test");

            Assert.That(result.Title, Is.Null);
            Assert.That(result.Year, Is.Null);
            Assert.That(result.Rated, Is.Null);
            Assert.That(result.Released, Is.Null);
            Assert.That(result.Runtime, Is.Null);
            Assert.That(result.Genre, Is.Null);
            Assert.That(result.Director, Is.Null);
            Assert.That(result.Writer, Is.Null);
            Assert.That(result.Actors, Is.Null);
            Assert.That(result.Plot, Is.Null);
            Assert.That(result.Language, Is.Null);
            Assert.That(result.Country, Is.Null);
            Assert.That(result.Awards, Is.Null);
            Assert.That(result.Poster, Is.Null);
            Assert.That(result.Ratings, Is.Null);
            Assert.That(result.Metascore, Is.Null);
            Assert.That(result.ImdbRating, Is.Null);
            Assert.That(result.ImdbVotes, Is.Null);
            Assert.That(result.ImdbId, Is.Null);
            Assert.That(result.Type, Is.Null);
            Assert.That(result.Dvd, Is.Null);
            Assert.That(result.BoxOffice, Is.Null);
            Assert.That(result.Production, Is.Null);
            Assert.That(result.Website, Is.Null);
            Assert.That(result.Response, Is.EqualTo("False"));
        }
    }
}
