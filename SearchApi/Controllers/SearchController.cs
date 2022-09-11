using Microsoft.AspNetCore.Mvc;
using SearchCore.Model;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSearchResults")]
        public IEnumerable<SearchableEntity> Get()
        {
            var dunmmies = new List<SearchableEntity>();
            var b = new Building();
            b.Name = "Test1";

            var l = new SmartLock();
            l.Name = "Test2";
            dunmmies.Add(b);
            dunmmies.Add(l);
            return dunmmies;
        }
    }
}
