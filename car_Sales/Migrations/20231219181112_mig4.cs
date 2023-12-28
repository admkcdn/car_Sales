using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_Sales.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "priceTransactions",
                newName: "CarsID");

            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "ımages",
                newName: "CarsID");

            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "carStatuses",
                newName: "CarsID");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "cars",
                newName: "TypesID");

            migrationBuilder.RenameColumn(
                name: "TranmissionID",
                table: "cars",
                newName: "TransmissionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_priceTransactions_CarsID",
                table: "priceTransactions",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_ımages_CarsID",
                table: "ımages",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_CarsID",
                table: "carStatuses",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_StatusID",
                table: "carStatuses",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_cars_FuelTypeID",
                table: "cars",
                column: "FuelTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_cars_TransmissionTypeID",
                table: "cars",
                column: "TransmissionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_cars_TypesID",
                table: "cars",
                column: "TypesID");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_fuelTypes_FuelTypeID",
                table: "cars",
                column: "FuelTypeID",
                principalTable: "fuelTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_transmissionTypes_TransmissionTypeID",
                table: "cars",
                column: "TransmissionTypeID",
                principalTable: "transmissionTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_types_TypesID",
                table: "cars",
                column: "TypesID",
                principalTable: "types",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carStatuses_cars_CarsID",
                table: "carStatuses",
                column: "CarsID",
                principalTable: "cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carStatuses_statuses_StatusID",
                table: "carStatuses",
                column: "StatusID",
                principalTable: "statuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ımages_cars_CarsID",
                table: "ımages",
                column: "CarsID",
                principalTable: "cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_priceTransactions_cars_CarsID",
                table: "priceTransactions",
                column: "CarsID",
                principalTable: "cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_fuelTypes_FuelTypeID",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_transmissionTypes_TransmissionTypeID",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_types_TypesID",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_carStatuses_cars_CarsID",
                table: "carStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_carStatuses_statuses_StatusID",
                table: "carStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ımages_cars_CarsID",
                table: "ımages");

            migrationBuilder.DropForeignKey(
                name: "FK_priceTransactions_cars_CarsID",
                table: "priceTransactions");

            migrationBuilder.DropIndex(
                name: "IX_priceTransactions_CarsID",
                table: "priceTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ımages_CarsID",
                table: "ımages");

            migrationBuilder.DropIndex(
                name: "IX_carStatuses_CarsID",
                table: "carStatuses");

            migrationBuilder.DropIndex(
                name: "IX_carStatuses_StatusID",
                table: "carStatuses");

            migrationBuilder.DropIndex(
                name: "IX_cars_FuelTypeID",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_TransmissionTypeID",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_TypesID",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "CarsID",
                table: "priceTransactions",
                newName: "CarID");

            migrationBuilder.RenameColumn(
                name: "CarsID",
                table: "ımages",
                newName: "CarID");

            migrationBuilder.RenameColumn(
                name: "CarsID",
                table: "carStatuses",
                newName: "CarID");

            migrationBuilder.RenameColumn(
                name: "TypesID",
                table: "cars",
                newName: "TypeID");

            migrationBuilder.RenameColumn(
                name: "TransmissionTypeID",
                table: "cars",
                newName: "TranmissionID");
        }
    }
}
