using System.Runtime.Serialization;

namespace OmdbServicesLibs.Models
{
	[DataContract]
	public class Search
	{
		public string Title { get; set; }
		public string Year { get; set; }
		[DataMember(Name = "imdbID")]
		public string ImdbID { get; set; }
		public string Poster { get; set; }
	}
}