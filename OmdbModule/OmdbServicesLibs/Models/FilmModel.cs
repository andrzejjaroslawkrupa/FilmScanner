using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Omdb.ServicesLibs.Models
{
    [DataContract]
    public class FilmModel
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "year")]
        public string Year { get; set; }
        [DataMember(Name = "rated")]
        public string Rated { get; set; }
        [DataMember(Name = "released")]
        public string Released { get; set; }
        [DataMember(Name = "runtime")]
        public string Runtime { get; set; }
        [DataMember(Name = "genre")]
        public string Genre { get; set; }
        [DataMember(Name = "director")]
        public string Director { get; set; }
        [DataMember(Name = "writer")]
        public string Writer { get; set; }
        [DataMember(Name = "actors")]
        public string Actors { get; set; }
        [DataMember(Name = "plot")]
        public string Plot { get; set; }
        [DataMember(Name = "language")]
        public string Language { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "awards")]
        public string Awards { get; set; }
        [DataMember(Name = "poster")]
        public string Poster { get; set; }
        [DataMember(Name = "ratings")]
        public IEnumerable<Ratings> Ratings { get; set; }
        [DataMember(Name = "metascore")]
        public string Metascore { get; set; }
        [DataMember(Name = "imdbRating")]
        public string ImdbRating { get; set; }
        [DataMember(Name = "imdbVotes")]
        public string ImdbVotes { get; set; }
        [DataMember(Name = "imdbID")]
        public string ImdbId { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "DVD")]
        public string Dvd { get; set; }
        [DataMember(Name = "boxOffice")]
        public string BoxOffice { get; set; }
        [DataMember(Name = "production")]
        public string Production { get; set; }
        [DataMember(Name = "website")]
        public string Website { get; set; }
        [DataMember(Name = "response")]
        public string Response { get; set; }
    }
}