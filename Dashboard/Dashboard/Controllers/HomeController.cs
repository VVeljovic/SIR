using System.Diagnostics;
using Dashboard.Interfaces;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAggregationRepository _repository;

        public HomeController(ILogger<HomeController> logger, IAggregationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var statsByDevice = _repository.GetSensorStatsByDevice();
            var statsByHour = _repository.GetSensorStatsByHours();

            var model = new DashboardViewModel
            {
                StatsByDevice = statsByDevice,
                StatsByHour = statsByHour,
                TotalMeasurements = statsByDevice.Sum(x => (long)x.Count),
                AvgTemperature = statsByDevice.Any() ? Math.Round(statsByDevice.Average(x => x.AvgTemperature), 1) : 0,
                AvgHumidity = statsByDevice.Any() ? Math.Round(statsByDevice.Average(x => x.AvgHumidity) * 100, 1) : 0,
            };

            return View(model);
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
