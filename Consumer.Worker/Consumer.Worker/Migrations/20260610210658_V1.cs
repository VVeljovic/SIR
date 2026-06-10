using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consumer.Worker.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccidentRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartLat = table.Column<double>(type: "double precision", nullable: true),
                    StartLng = table.Column<double>(type: "double precision", nullable: true),
                    EndLat = table.Column<double>(type: "double precision", nullable: true),
                    EndLng = table.Column<double>(type: "double precision", nullable: true),
                    DistanceMi = table.Column<double>(type: "double precision", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    County = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Zipcode = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Timezone = table.Column<string>(type: "text", nullable: false),
                    AirportCode = table.Column<string>(type: "text", nullable: false),
                    WeatherTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TemperatureF = table.Column<double>(type: "double precision", nullable: true),
                    WindChillF = table.Column<double>(type: "double precision", nullable: true),
                    HumidityPct = table.Column<double>(type: "double precision", nullable: true),
                    PressureIn = table.Column<double>(type: "double precision", nullable: true),
                    VisibilityMi = table.Column<double>(type: "double precision", nullable: true),
                    WindDirection = table.Column<string>(type: "text", nullable: false),
                    WindSpeedMph = table.Column<double>(type: "double precision", nullable: true),
                    PrecipitationIn = table.Column<double>(type: "double precision", nullable: true),
                    WeatherCondition = table.Column<string>(type: "text", nullable: false),
                    Amenity = table.Column<bool>(type: "boolean", nullable: false),
                    Bump = table.Column<bool>(type: "boolean", nullable: false),
                    Crossing = table.Column<bool>(type: "boolean", nullable: false),
                    GiveWay = table.Column<bool>(type: "boolean", nullable: false),
                    Junction = table.Column<bool>(type: "boolean", nullable: false),
                    NoExit = table.Column<bool>(type: "boolean", nullable: false),
                    Railway = table.Column<bool>(type: "boolean", nullable: false),
                    Roundabout = table.Column<bool>(type: "boolean", nullable: false),
                    Station = table.Column<bool>(type: "boolean", nullable: false),
                    Stop = table.Column<bool>(type: "boolean", nullable: false),
                    TrafficCalming = table.Column<bool>(type: "boolean", nullable: false),
                    TrafficSignal = table.Column<bool>(type: "boolean", nullable: false),
                    TurningLoop = table.Column<bool>(type: "boolean", nullable: false),
                    SunriseSunset = table.Column<string>(type: "text", nullable: false),
                    CivilTwilight = table.Column<string>(type: "text", nullable: false),
                    NauticalTwilight = table.Column<string>(type: "text", nullable: false),
                    AstronomicalTwilight = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccidentsByState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    AvgSeverity = table.Column<double>(type: "double precision", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentsByState", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentRecords");

            migrationBuilder.DropTable(
                name: "AccidentsByState");
        }
    }
}
