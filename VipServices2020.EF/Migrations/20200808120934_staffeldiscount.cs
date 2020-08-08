using Microsoft.EntityFrameworkCore.Migrations;

namespace VipServices2020.EF.Migrations
{
    public partial class staffeldiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffels",
                columns: table => new
                {
                    NumberOfBookedReservations = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(nullable: false),
                    DiscountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffels", x => x.NumberOfBookedReservations);
                    table.ForeignKey(
                        name: "FK_Staffels_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffels_DiscountId",
                table: "Staffels",
                column: "DiscountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staffels");

            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
