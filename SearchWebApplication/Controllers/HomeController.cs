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
            var results = await _service.GetSearchResultAsync(searchKey);
            return View("Results", results);            

        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}