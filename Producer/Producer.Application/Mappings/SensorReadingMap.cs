using CsvHelper.Configuration;
using Producer.Domain.Models;

namespace Producer.Application.Mappings
{
    public class SensorReadingMap : ClassMap<SensorReading>
    {
        public SensorReadingMap() 
        {
            Map(x => x.Timestamp).Name("ts").TypeConverter<UnixTimestampConverter>();
            Map(x => x.Device).Name("device");
            Map(x => x.Co).Name("co");
            Map(x => x.Humidity).Name("humidity");
            Map(x => x.Light).Name("light");
            Map(x => x.Lpg).Name("lpg");
            Map(x => x.Motion).Name("motion");
            Map(x => x.Smoke).Name("smoke");
            Map(x => x.Temperature).Name("temp");
        }
    }
}
