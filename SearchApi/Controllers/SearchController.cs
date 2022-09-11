using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchCore;
using SearchCore.Model;
using System.Reflection;

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
        public async Task<IEnumerable<SearchableEntity>> Get(string searchKey)
        {
            var data = await GetDataAsync();
            if(data == null) return new List<SearchableEntity>();

            var search = new Search(data);
            var result = search.GetResults(searchKey);
            return result;
            
        }

        private async Task<SearchDataSet> GetDataAsync()
        {
            SearchDataSet? data = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(@"https://simonsvoss-homework.herokuapp.com/sv_lsm_data.json");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<SearchDataSet>(jsonString);
            }
            return data;
        }
    }
}
