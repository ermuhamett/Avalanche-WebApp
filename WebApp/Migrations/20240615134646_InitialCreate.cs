using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "AvalancheData",
                columns: table => new
                {
                    DataId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    AirTemperatureMorning = table.Column<decimal>(type: "numeric", nullable: false),
                    SnowDepthAverageMorning = table.Column<decimal>(type: "numeric", nullable: false),
                    SnowDepthMaxMorning = table.Column<decimal>(type: "numeric", nullable: false),
                    WeatherMorning = table.Column<string>(type: "text", nullable: false),
                    AirTemperatureEvening = table.Column<decimal>(type: "numeric", nullable: false),
                    SnowDepthAverageEvening = table.Column<decimal>(type: "numeric", nullable: false),
                    SnowDepthMaxEvening = table.Column<decimal>(type: "numeric", nullable: false),
                    WeatherEvening = table.Column<string>(type: "text", nullable: false),
                    AverageTemperatureDay = table.Column<decimal>(type: "numeric", nullable: false),
                    AverageTemperatureDecade = table.Column<decimal>(type: "numeric", nullable: false),
                    Precipitation = table.Column<decimal>(type: "numeric", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvalancheData", x => x.DataId);
                    table.ForeignKey(
                        name: "FK_AvalancheData_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvalancheData_RegionId",
                table: "AvalancheData",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvalancheData");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
