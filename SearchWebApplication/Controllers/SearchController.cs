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
        public async Task<ActionResult<SearchableEntities>> Get(string searchKey)
        {
            var data = await GetDataAsync();
            if (data == null) return new ObjectResult(string.Empty);

            var search = new Search(data);
            var result = search.GetResults(searchKey);

            var list = new List<dynamic>();
            foreach (var item in result)
            {
                list.Add(item);
                //if (item is SearchableEntity<Building> si)
                //{
                //    list.Add(si.Data);
                //}
                //if (item is SearchableEntity<SmartLock> si1)
                //{
                //    list.Add(si1.Data);
                //}
                //if (item is SearchableEntity<Group> si2)
                //{
                //    list.Add(si2.Data);
                //}
                //if (item is SearchableEntity<Medium> si3)
                //{
                //    list.Add(si3.Data);
                //}
            }
            var teat = new SearchableEntities();
            teat.Items = list;
            
            return new ObjectResult(teat);
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
