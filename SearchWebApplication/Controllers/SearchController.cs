using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchCore;
using SearchCore.Model;

namespace SearchWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        
        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSearchResults")]
        public async Task<ActionResult<IEnumerable<SearchableEntityDTO>>> Get(string searchKey)
        {
            var data = await GetDataAsync();
            if (data == null) return new ObjectResult(string.Empty);

            var search = new Search(data);
            var result = search.GetResults(searchKey);           
            
            return new ObjectResult(result);
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
