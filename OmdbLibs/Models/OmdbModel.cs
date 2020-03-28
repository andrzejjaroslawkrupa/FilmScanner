using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OmdbLibs.Models
{
	[DataContract]
	public class OmdbModel
	{
		[DataMember(Name = "Search")]
		public IEnumerable<Search> Searches { get; set; }
		[DataMember(Name = "totalResults")]
		public string TotalResults { get; set; }
		[DataMember(Name = "response")]
		public string Response { get; set; }
	}
}
