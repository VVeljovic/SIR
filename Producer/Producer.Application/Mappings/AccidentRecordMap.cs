using CsvHelper.Configuration;
using Producer.Domain.Entities;

namespace Producer.Application.Mappings
{
    public class AccidentRecordMap : ClassMap<AccidentRecord>
    {
        public AccidentRecordMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Source).Name("Source");
            Map(m => m.Severity).Name("Severity");
            Map(m => m.StartTime).Name("Start_Time");   
            Map(m => m.EndTime).Name("End_Time");
            Map(m => m.StartLat).Name("Start_Lat");
            Map(m => m.StartLng).Name("Start_Lng");
            Map(m => m.EndLat).Name("End_Lat");
            Map(m => m.EndLng).Name("End_Lng");
            Map(m => m.DistanceMi).Name("Distance(mi)");
            Map(m => m.Description).Name("Description");
            Map(m => m.Street).Name("Street");
            Map(m => m.City).Name("City");
            Map(m => m.County).Name("County");
            Map(m => m.State).Name("State");
            Map(m => m.Zipcode).Name("Zipcode");
            Map(m => m.Country).Name("Country");
            Map(m => m.Timezone).Name("Timezone");
            Map(m => m.AirportCode).Name("Airport_Code");
            Map(m => m.WeatherTimestamp).Name("Weather_Timestamp");
            Map(m => m.TemperatureF).Name("Temperature(F)");
            Map(m => m.WindChillF).Name("Wind_Chill(F)");
            Map(m => m.HumidityPct).Name("Humidity(%)");
            Map(m => m.PressureIn).Name("Pressure(in)");
            Map(m => m.VisibilityMi).Name("Visibility(mi)");
            Map(m => m.WindDirection).Name("Wind_Direction");
            Map(m => m.WindSpeedMph).Name("Wind_Speed(mph)");
            Map(m => m.PrecipitationIn).Name("Precipitation(in)");
            Map(m => m.WeatherCondition).Name("Weather_Condition");
            Map(m => m.Amenity).Name("Amenity");
            Map(m => m.Bump).Name("Bump");
            Map(m => m.Crossing).Name("Crossing");
            Map(m => m.GiveWay).Name("Give_Way");
            Map(m => m.Junction).Name("Junction");
            Map(m => m.NoExit).Name("No_Exit");
            Map(m => m.Railway).Name("Railway");
            Map(m => m.Roundabout).Name("Roundabout");
            Map(m => m.Station).Name("Station");
            Map(m => m.Stop).Name("Stop");
            Map(m => m.TrafficCalming).Name("Traffic_Calming");
            Map(m => m.TrafficSignal).Name("Traffic_Signal");
            Map(m => m.TurningLoop).Name("Turning_Loop");
            Map(m => m.SunriseSunset).Name("Sunrise_Sunset");
            Map(m => m.CivilTwilight).Name("Civil_Twilight");
            Map(m => m.NauticalTwilight).Name("Nautical_Twilight");
            Map(m => m.AstronomicalTwilight).Name("Astronomical_Twilight");
        }
    }
}
