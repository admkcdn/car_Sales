using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_Sales.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarParts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "fuelTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuelTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "transmissionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transmissionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Kilometer = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false),
                    EngineVolume = table.Column<int>(type: "int", nullable: false),
                    TypesID = table.Column<int>(type: "int", nullable: false),
                    TransmissionTypeID = table.Column<int>(type: "int", nullable: false),
                    FuelTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_cars_fuelTypes_FuelTypeID",
                        column: x => x.FuelTypeID,
                        principalTable: "fuelTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_transmissionTypes_TransmissionTypeID",
                        column: x => x.TransmissionTypeID,
                        principalTable: "transmissionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_types_TypesID",
                        column: x => x.TypesID,
                        principalTable: "types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adverts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false),
                    CarsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adverts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_adverts_cars_CarsID",
                        column: x => x.CarsID,
                        principalTable: "cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_adverts_users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarPartsID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarsID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_carStatuses_CarParts_CarPartsID",
                        column: x => x.CarPartsID,
                        principalTable: "CarParts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carStatuses_cars_CarsID",
                        column: x => x.CarsID,
                        principalTable: "cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carStatuses_statuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "statuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ımages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarsID = table.Column<int>(type: "int", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ımages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ımages_cars_CarsID",
                        column: x => x.CarsID,
                        principalTable: "cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "priceTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarsID = table.Column<int>(type: "int", nullable: false),
                    OldPrice = table.Column<float>(type: "real", nullable: false),
                    NewPrice = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priceTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_priceTransactions_cars_CarsID",
                        column: x => x.CarsID,
                        principalTable: "cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adverts_CarsID",
                table: "adverts",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_adverts_UsersID",
                table: "adverts",
                column: "UsersID");

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

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_CarPartsID",
                table: "carStatuses",
                column: "CarPartsID");

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_CarsID",
                table: "carStatuses",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_carStatuses_StatusID",
                table: "carStatuses",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ımages_CarsID",
                table: "ımages",
                column: "CarsID");

            migrationBuilder.CreateIndex(
                name: "IX_priceTransactions_CarsID",
                table: "priceTransactions",
                column: "CarsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adverts");

            migrationBuilder.DropTable(
                name: "carStatuses");

            migrationBuilder.DropTable(
                name: "ımages");

            migrationBuilder.DropTable(
                name: "priceTransactions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "CarParts");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "fuelTypes");

            migrationBuilder.DropTable(
                name: "transmissionTypes");

            migrationBuilder.DropTable(
                name: "types");
        }
    }
}
