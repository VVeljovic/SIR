using ClosedXML.Excel;
using Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController(IAggregationRepository repository) : ControllerBase
    {
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var readings = await repository.GetAllReadingsAsync();
            var statsByDevice = await repository.GetSensorStatsByDeviceAsync();
            var statsByHour = await repository.GetSensorStatsByHoursAsync();

            using var workbook = new XLWorkbook();

            // Sheet 1 - Raw Readings
            var sheet1 = workbook.Worksheets.Add("Raw Readings");
            sheet1.Cell(1, 1).Value = "Timestamp";
            sheet1.Cell(1, 2).Value = "Device";
            sheet1.Cell(1, 3).Value = "Temperature";
            sheet1.Cell(1, 4).Value = "Humidity";
            sheet1.Cell(1, 5).Value = "Co";
            sheet1.Cell(1, 6).Value = "Lpg";
            sheet1.Cell(1, 7).Value = "Smoke";
            sheet1.Cell(1, 8).Value = "Light";
            sheet1.Cell(1, 9).Value = "Motion";

            for (int i = 0; i < readings.Count; i++)
            {
                var r = readings[i];
                sheet1.Cell(i + 2, 1).Value = r.Timestamp;
                sheet1.Cell(i + 2, 2).Value = r.Device;
                sheet1.Cell(i + 2, 3).Value = r.Temperature;
                sheet1.Cell(i + 2, 4).Value = r.Humidity;
                sheet1.Cell(i + 2, 5).Value = r.Co;
                sheet1.Cell(i + 2, 6).Value = r.Lpg;
                sheet1.Cell(i + 2, 7).Value = r.Smoke;
                sheet1.Cell(i + 2, 8).Value = r.Light;
                sheet1.Cell(i + 2, 9).Value = r.Motion;
            }

            // Sheet 2 - Anomaly Report
            double Avg(Func<Dashboard.Data.SensorReading, double> f) => readings.Average(f);
            double StdDev(Func<Dashboard.Data.SensorReading, double> f)
            {
                var avg = Avg(f);
                return Math.Sqrt(readings.Average(x => Math.Pow(f(x) - avg, 2)));
            }
            double ZScore(double value, double avg, double stdDev) =>
                stdDev == 0 ? 0 : (value - avg) / stdDev;

            var avgTemp = Avg(x => x.Temperature);   var stdTemp = StdDev(x => x.Temperature);
            var avgCo = Avg(x => x.Co);              var stdCo = StdDev(x => x.Co);
            var avgSmoke = Avg(x => x.Smoke);        var stdSmoke = StdDev(x => x.Smoke);

            var sheet2 = workbook.Worksheets.Add("Anomaly Report");
            sheet2.Cell(1, 1).Value = "Timestamp";
            sheet2.Cell(1, 2).Value = "Device";
            sheet2.Cell(1, 3).Value = "Temperature";
            sheet2.Cell(1, 4).Value = "ZScore_Temp";
            sheet2.Cell(1, 5).Value = "Co";
            sheet2.Cell(1, 6).Value = "ZScore_Co";
            sheet2.Cell(1, 7).Value = "Smoke";
            sheet2.Cell(1, 8).Value = "ZScore_Smoke";
            sheet2.Cell(1, 9).Value = "IsAnomaly";

            for (int i = 0; i < readings.Count; i++)
            {
                var r = readings[i];
                var zTemp = ZScore(r.Temperature, avgTemp, stdTemp);
                var zCo = ZScore(r.Co, avgCo, stdCo);
                var zSmoke = ZScore(r.Smoke, avgSmoke, stdSmoke);
                var isAnomaly = Math.Abs(zTemp) > 2 || Math.Abs(zCo) > 2 || Math.Abs(zSmoke) > 2;

                sheet2.Cell(i + 2, 1).Value = r.Timestamp;
                sheet2.Cell(i + 2, 2).Value = r.Device;
                sheet2.Cell(i + 2, 3).Value = r.Temperature;
                sheet2.Cell(i + 2, 4).Value = Math.Round(zTemp, 3);
                sheet2.Cell(i + 2, 5).Value = r.Co;
                sheet2.Cell(i + 2, 6).Value = Math.Round(zCo, 3);
                sheet2.Cell(i + 2, 7).Value = r.Smoke;
                sheet2.Cell(i + 2, 8).Value = Math.Round(zSmoke, 3);
                sheet2.Cell(i + 2, 9).Value = isAnomaly;
            }

            // Sheet 3 - Summary po uredaju
            var sheet3 = workbook.Worksheets.Add("Summary By Device");
            sheet3.Cell(1, 1).Value = "Device";
            sheet3.Cell(1, 2).Value = "Count";
            sheet3.Cell(1, 3).Value = "AvgTemp";
            sheet3.Cell(1, 4).Value = "MinTemp";
            sheet3.Cell(1, 5).Value = "MaxTemp";
            sheet3.Cell(1, 6).Value = "StdDevTemp";
            sheet3.Cell(1, 7).Value = "AvgCo";
            sheet3.Cell(1, 8).Value = "AvgSmoke";
            sheet3.Cell(1, 9).Value = "AnomalyCount";

            var grouped = readings.GroupBy(x => x.Device).ToList();
            for (int i = 0; i < grouped.Count; i++)
            {
                var g = grouped[i];
                var list = g.ToList();
                var avgT = list.Average(x => x.Temperature);
                var stdT = Math.Sqrt(list.Average(x => Math.Pow(x.Temperature - avgT, 2)));
                var avgC = list.Average(x => x.Co);
                var avgS = list.Average(x => x.Smoke);
                var anomalies = list.Count(x =>
                    Math.Abs(ZScore(x.Temperature, avgTemp, stdTemp)) > 2 ||
                    Math.Abs(ZScore(x.Co, avgCo, stdCo)) > 2 ||
                    Math.Abs(ZScore(x.Smoke, avgSmoke, stdSmoke)) > 2);

                sheet3.Cell(i + 2, 1).Value = g.Key;
                sheet3.Cell(i + 2, 2).Value = list.Count;
                sheet3.Cell(i + 2, 3).Value = Math.Round(avgT, 3);
                sheet3.Cell(i + 2, 4).Value = Math.Round(list.Min(x => x.Temperature), 3);
                sheet3.Cell(i + 2, 5).Value = Math.Round(list.Max(x => x.Temperature), 3);
                sheet3.Cell(i + 2, 6).Value = Math.Round(stdT, 3);
                sheet3.Cell(i + 2, 7).Value = Math.Round(avgC, 6);
                sheet3.Cell(i + 2, 8).Value = Math.Round(avgS, 6);
                sheet3.Cell(i + 2, 9).Value = anomalies;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "sensor-report.xlsx");
        }
    }
}
