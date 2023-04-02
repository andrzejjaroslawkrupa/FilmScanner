using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FilmScanner.Entities.Models
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<FilmRecord> FilmRecords { get; set; }
    }
}