using System.Runtime.Serialization;

namespace OmdbLibs.Models
{
	[DataContract]
	public class Search
	{
		[DataMember(Name = "title")]
		public string Title { get; set; }
		[DataMember(Name = "year")]
		public string Year { get; set; }
		[DataMember(Name = "imdbID")]
		public string ImdbID { get; set; }
		[DataMember(Name = "poster")]
		public string Poster { get; set; }
	}
}