using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_Sales.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarID",
                table: "adverts");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "adverts",
                newName: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_adverts_CarsID",
                table: "adverts",
                column: "CarsID");

            migrationBuilder.AddForeignKey(
                name: "FK_adverts_cars_CarsID",
                table: "adverts",
                column: "CarsID",
                principalTable: "cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adverts_cars_CarsID",
                table: "adverts");

            migrationBuilder.DropIndex(
                name: "IX_adverts_CarsID",
                table: "adverts");

            migrationBuilder.RenameColumn(
                name: "CarsID",
                table: "adverts",
                newName: "UserID");

            migrationBuilder.AddColumn<int>(
                name: "CarID",
                table: "adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
