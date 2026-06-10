
namespace Producer.Domain.Entities
{
    public sealed class AccidentRecord
    {
        public string Id { get; init; }
        public string Source { get; init; }
        public int Severity { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
        public double? StartLat { get; init; }
        public double? StartLng { get; init; }
        public double? EndLat { get; init; }
        public double? EndLng { get; init; }
        public double? DistanceMi { get; init; }
        public string Description { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string County { get; init; }
        public string State { get; init; }
        public string Zipcode { get; init; }
        public string Country { get; init; }
        public string Timezone { get; init; }
        public string AirportCode { get; init; }
        public DateTime? WeatherTimestamp { get; init; }
        public double? TemperatureF { get; init; }
        public double? WindChillF { get; init; }
        public double? HumidityPct { get; init; }
        public double? PressureIn { get; init; }
        public double? VisibilityMi { get; init; }
        public string WindDirection { get; init; }
        public double? WindSpeedMph { get; init; }
        public double? PrecipitationIn { get; init; }
        public string WeatherCondition { get; init; }
        public bool Amenity { get; init; }
        public bool Bump { get; init; }
        public bool Crossing { get; init; }
        public bool GiveWay { get; init; }
        public bool Junction { get; init; }
        public bool NoExit { get; init; }
        public bool Railway { get; init; }
        public bool Roundabout { get; init; }
        public bool Station { get; init; }
        public bool Stop { get; init; }
        public bool TrafficCalming { get; init; }
        public bool TrafficSignal { get; init; }
        public bool TurningLoop { get; init; }
        public string SunriseSunset { get; init; }
        public string CivilTwilight { get; init; }
        public string NauticalTwilight { get; init; }
        public string AstronomicalTwilight { get; init; }
    }
}
