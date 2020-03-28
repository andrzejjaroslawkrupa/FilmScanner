using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FilmScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OmdbController : ControllerBase
    {
        private readonly IConfiguration _Configuration;

        public OmdbController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // GET: api/Omdb
        [HttpGet]
        public async Task<ActionResult> GetOmdb()
        {
            string url = "https://www.omdbapi.com/?s=spider-man&apikey=" + _Configuration["Omdb_api_key"] ;
            RedirectResult redirectResult = new RedirectResult(url, true);

            return redirectResult;
        }

    }
}