using System;
using System.Web;
using NUnit.Framework;
using Omdb.ServicesLibs.Omdb;

namespace Omdb.ServicesLibs.Tests
{
    [TestFixture]
    public class OmdbUrlTests
    {
        [TestCase("matrix")]
        [TestCase("fast & furious")]
        [TestCase("#9")]
        [TestCase("AC/DC")]
        [TestCase("what?")]
        [TestCase("100%")]
        [TestCase("a=b+c")]
        [TestCase("Amélie")]
        public void SearchUrl_SpecialCharacters_ArriveAsSingleSearchValue(string title)
        {
            var url = GetSearchResults.BuildUrl(title, 2, "key");
            var query = HttpUtility.ParseQueryString(url.Query);

            Assert.That(url.Fragment, Is.Empty);
            Assert.That(query["s"], Is.EqualTo(title));
            Assert.That(query["apikey"], Is.EqualTo("key"));
            Assert.That(query["page"], Is.EqualTo("2"));
            Assert.That(query.Count, Is.EqualTo(3));
        }

        [Test]
        public void SearchUrl_NoPage_EmptyPageParameter()
        {
            var query = HttpUtility.ParseQueryString(GetSearchResults.BuildUrl("matrix", null, "key").Query);

            Assert.That(query["page"], Is.Empty);
        }

        [TestCase("tt0133093")]
        [TestCase("tt0133093&plot=full")]
        public void FilmUrl_ImdbId_ArrivesAsSingleIdValue(string imdbId)
        {
            var url = GetFilmById.BuildUrl(imdbId, "key");
            var query = HttpUtility.ParseQueryString(url.Query);

            Assert.That(query["i"], Is.EqualTo(imdbId));
            Assert.That(query["apikey"], Is.EqualTo("key"));
            Assert.That(query.Count, Is.EqualTo(2));
        }
    }
}
