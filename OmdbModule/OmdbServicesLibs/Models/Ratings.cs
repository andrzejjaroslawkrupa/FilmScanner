﻿using System.Runtime.Serialization;

namespace Omdb.ServicesLibs.Models
{
    public class Ratings
    {
        [DataMember(Name = "source")]
        public string Source { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}