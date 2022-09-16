using Newtonsoft.Json;
using SearchCore.Model;
using System.Collections;
using System.Dynamic;
using System.Reflection;

namespace SearchWebApplication
{
    public class SearchService
    {
        readonly string uri = "https://localhost:7265/Search?searchKey=";

        public async Task<SearchableEntities> GetSearchResultAsync(string searchKey)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var url = uri + searchKey;
                var resultString = await httpClient.GetStringAsync(url);

                var result = JsonConvert.DeserializeObject<SearchableEntities>(resultString);
                
                return result;
            }
            
        }
    }

    
}

