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
                name: "SensorReading",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Device = table.Column<string>(type: "text", nullable: false),
                    Co = table.Column<double>(type: "double precision", nullable: false),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    Light = table.Column<bool>(type: "boolean", nullable: false),
                    Lpg = table.Column<double>(type: "double precision", nullable: false),
                    Motion = table.Column<bool>(type: "boolean", nullable: false),
                    Smoke = table.Column<double>(type: "double precision", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorReading", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorStatsByDevice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Device = table.Column<string>(type: "text", nullable: false),
                    AvgCo = table.Column<double>(type: "double precision", nullable: false),
                    AvgHumidity = table.Column<double>(type: "double precision", nullable: false),
                    AvgLpg = table.Column<double>(type: "double precision", nullable: false),
                    AvgSmoke = table.Column<double>(type: "double precision", nullable: false),
                    AvgTemperature = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorStatsByDevice", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorReading");

            migrationBuilder.DropTable(
                name: "SensorStatsByDevice");
        }
    }
}
