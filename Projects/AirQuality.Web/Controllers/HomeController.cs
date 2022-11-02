using System.Diagnostics;
using AirQuality.Domain.LatestMeasurements;
using AirQuality.Integration;
using AirQuality.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirQualityService _airQualityService;
        private readonly ILogger<HomeController> _logger;
        private readonly ILatestMeasurementsViewModelFactory _latestMeasurementsViewModelFactory;

        public HomeController(ILogger<HomeController> logger, ILatestMeasurementsViewModelFactory latestMeasurementsViewModelFactory)
        {
            _logger = logger;
            _latestMeasurementsViewModelFactory = latestMeasurementsViewModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var vm = _latestMeasurementsViewModelFactory.CreateGetModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            var vm = await _latestMeasurementsViewModelFactory.CreatePostModelAsync(city);
            return View(vm);
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