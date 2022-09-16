using Microsoft.AspNetCore.Mvc;
using SearchWebApplication.Models;
using System.Diagnostics;

namespace SearchWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private SearchService _service = new SearchService();
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchKey)
        {
            return View("Results",
            await _service.GetSearchResultAsync(searchKey)
        );

            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await response.Content.ReadAsStringAsync();
            //        var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

            //    }


            //}
            return View();

        }
        public IActionResult Privacy()
        {
            return View();
        }

        public ViewResult Search()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}