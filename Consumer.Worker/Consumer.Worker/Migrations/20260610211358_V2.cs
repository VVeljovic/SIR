using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consumer.Worker.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccidentsByState_State",
                table: "AccidentsByState",
                column: "State",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccidentsByState_State",
                table: "AccidentsByState");
        }
    }
}
