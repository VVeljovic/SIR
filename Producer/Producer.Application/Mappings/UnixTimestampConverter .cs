using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace Producer.Application.Mappings
{
    public class UnixTimestampConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var unixSeconds = double.Parse(text, CultureInfo.InvariantCulture);
            return DateTimeOffset
                .FromUnixTimeMilliseconds((long)(unixSeconds * 1000))
                .UtcDateTime;
        }
    }
}
