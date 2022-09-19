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
        private Search _search;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
            _search = new Search();
        }

        [HttpGet(Name = "GetSearchResults")]
        public async Task<IEnumerable<SearchableEntityDTO>> Get(string searchKey)
        {
            if(string.IsNullOrEmpty(searchKey)) return Enumerable.Empty<SearchableEntityDTO>();

            var t = Task.Run(() => GetDataAsync())
                .ContinueWith(r => _search.GetSearchResults(searchKey, r.Result));
            
            return t.Result;
            
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
