using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForSale_City_CityId1",
                table: "ForSale");

            migrationBuilder.DropIndex(
                name: "IX_ForSale_CityId1",
                table: "ForSale");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "ForSale");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "ForSale",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ForSale_CityId",
                table: "ForSale",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForSale_City_CityId",
                table: "ForSale",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForSale_City_CityId",
                table: "ForSale");

            migrationBuilder.DropIndex(
                name: "IX_ForSale_CityId",
                table: "ForSale");

            migrationBuilder.AlterColumn<string>(
                name: "CityId",
                table: "ForSale",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "ForSale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ForSale_CityId1",
                table: "ForSale",
                column: "CityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ForSale_City_CityId1",
                table: "ForSale",
                column: "CityId1",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
