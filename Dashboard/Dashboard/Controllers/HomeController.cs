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

        public async Task<IActionResult> Index()
        {
            var statsByDevice = await _repository.GetSensorStatsByDeviceAsync();
            var statsByHour = await _repository.GetSensorStatsByHoursAsync();

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

        [HttpGet("/health")]
        public IActionResult Health() => Ok("healthy");

        [HttpGet("/api/cpu-stress")]
        public IActionResult CpuStress()
        {
            var limit = 10_000_000;
            var sieve = new bool[limit + 1];
            Array.Fill(sieve, true);
            sieve[0] = sieve[1] = false;

            for (int i = 2; i * i <= limit; i++)
                if (sieve[i])
                    for (int j = i * i; j <= limit; j += i)
                        sieve[j] = false;

            var count = sieve.Count(x => x);
            return Ok(new { PrimesFound = count, UpTo = limit });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
