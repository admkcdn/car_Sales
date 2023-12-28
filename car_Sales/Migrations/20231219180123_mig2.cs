using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car_Sales.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersID",
                table: "adverts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_adverts_UsersID",
                table: "adverts",
                column: "UsersID");

            migrationBuilder.AddForeignKey(
                name: "FK_adverts_users_UsersID",
                table: "adverts",
                column: "UsersID",
                principalTable: "users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adverts_users_UsersID",
                table: "adverts");

            migrationBuilder.DropIndex(
                name: "IX_adverts_UsersID",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "UsersID",
                table: "adverts");
        }
    }
}
