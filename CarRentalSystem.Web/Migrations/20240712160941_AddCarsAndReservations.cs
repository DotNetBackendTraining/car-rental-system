using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCarsAndReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ImageName", "Location", "Make", "Model", "Year" },
                values: new object[,]
                {
                    { 1, "2k55x9_1280x960-min.png", "New York", "Toyota", "Camry", 2020 },
                    { 2, "2k5559_1280x960-min.png", "Los Angeles", "Honda", "Civic", 2019 },
                    { 3, "3keee6_1280x960-min.png", "Chicago", "Ford", "Mustang", 2021 },
                    { 4, "5gw135_1280x960-min.png", "Houston", "Chevrolet", "Malibu", 2020 },
                    { 5, "d5qqjo_1280x960-min.png", "Phoenix", "Nissan", "Altima", 2018 },
                    { 6, "d5qqz3_1280x960-min.png", "Philadelphia", "BMW", "3 Series", 2021 },
                    { 7, "gjllll_1280x960-min.png", "San Antonio", "Mercedes-Benz", "C-Class", 2019 },
                    { 8, "w8rrj6_1280x960-min.png", "San Diego", "Audi", "A4", 2020 },
                    { 9, "x122yv_1280x960-min.png", "Dallas", "Lexus", "ES", 2021 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
