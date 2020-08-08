using Microsoft.EntityFrameworkCore.Migrations;

namespace VipServices2020.EF.Migrations
{
    public partial class staffel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffelNumberOfBookedReservations",
                table: "Prices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_StaffelNumberOfBookedReservations",
                table: "Prices",
                column: "StaffelNumberOfBookedReservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Staffels_StaffelNumberOfBookedReservations",
                table: "Prices",
                column: "StaffelNumberOfBookedReservations",
                principalTable: "Staffels",
                principalColumn: "NumberOfBookedReservations",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Staffels_StaffelNumberOfBookedReservations",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_StaffelNumberOfBookedReservations",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "StaffelNumberOfBookedReservations",
                table: "Prices");
        }
    }
}
