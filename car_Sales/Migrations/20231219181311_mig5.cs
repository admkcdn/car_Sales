using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_Sales.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarParts",
                table: "carStatuses",
                newName: "CarPartsID");

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_CarPartsID",
                table: "carStatuses",
                column: "CarPartsID");

            migrationBuilder.AddForeignKey(
                name: "FK_carStatuses_CarParts_CarPartsID",
                table: "carStatuses",
                column: "CarPartsID",
                principalTable: "CarParts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carStatuses_CarParts_CarPartsID",
                table: "carStatuses");

            migrationBuilder.DropIndex(
                name: "IX_carStatuses_CarPartsID",
                table: "carStatuses");

            migrationBuilder.RenameColumn(
                name: "CarPartsID",
                table: "carStatuses",
                newName: "CarParts");
        }
    }
}
