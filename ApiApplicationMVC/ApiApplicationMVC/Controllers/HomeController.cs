using ApiApplicationMVC.Models;
using ApiApplicationMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            List<Person> apiData = await _apiService.GetApiDataAsync();
            if (apiData != null)
            {
                return View(apiData);
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult Privacy()
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