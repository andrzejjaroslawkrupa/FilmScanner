using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Omdb.ServicesLibs.Models
{
    [DataContract]
    public class SearchResultModel
    {
        [DataMember(Name = "Search")]
        public IEnumerable<Search> Searches { get; set; }
        [DataMember(Name = "totalResults")]
        public string TotalResults { get; set; }
        [DataMember(Name = "response")]
        public string Response { get; set; }
    }
}
