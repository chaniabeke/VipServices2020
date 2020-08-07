using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VipServices2020.EF.Migrations
{
    public partial class initcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<string>(maxLength: 5, nullable: false),
                    Town = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Limousines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Color = table.Column<string>(maxLength: 50, nullable: false),
                    FirstHourPrice = table.Column<int>(nullable: false),
                    NightLifePrice = table.Column<int>(nullable: false),
                    WeddingPrice = table.Column<int>(nullable: false),
                    WelnessPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limousines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Town = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstHourPrice = table.Column<int>(nullable: false),
                    SecondHourCount = table.Column<int>(nullable: false),
                    SecondHourPrice = table.Column<decimal>(nullable: false),
                    NightHourCount = table.Column<int>(nullable: false),
                    NightHourPrice = table.Column<decimal>(nullable: false),
                    OvertimeCount = table.Column<int>(nullable: false),
                    OvertimePrice = table.Column<decimal>(nullable: false),
                    FixedPrice = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    ExclusiveBtw = table.Column<decimal>(nullable: false),
                    BtwPrice = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BtwNumber = table.Column<string>(maxLength: 16, nullable: true),
                    AddressId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerNumber);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNumber = table.Column<int>(nullable: true),
                    ReservationCreated = table.Column<DateTime>(nullable: false),
                    LimousineExpectedAddressId = table.Column<int>(nullable: true),
                    StartLocationId = table.Column<int>(nullable: true),
                    ArrivalLocationId = table.Column<int>(nullable: true),
                    ArrangementType = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    TotalHours = table.Column<TimeSpan>(nullable: false),
                    LimousineId = table.Column<int>(nullable: true),
                    PriceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_ArrivalLocationId",
                        column: x => x.ArrivalLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerNumber",
                        column: x => x.CustomerNumber,
                        principalTable: "Customers",
                        principalColumn: "CustomerNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Addresses_LimousineExpectedAddressId",
                        column: x => x.LimousineExpectedAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Limousines_LimousineId",
                        column: x => x.LimousineId,
                        principalTable: "Limousines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_StartLocationId",
                        column: x => x.StartLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CategoryId",
                table: "Customers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ArrivalLocationId",
                table: "Reservations",
                column: "ArrivalLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerNumber",
                table: "Reservations",
                column: "CustomerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LimousineExpectedAddressId",
                table: "Reservations",
                column: "LimousineExpectedAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LimousineId",
                table: "Reservations",
                column: "LimousineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PriceId",
                table: "Reservations",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StartLocationId",
                table: "Reservations",
                column: "StartLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Limousines");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
