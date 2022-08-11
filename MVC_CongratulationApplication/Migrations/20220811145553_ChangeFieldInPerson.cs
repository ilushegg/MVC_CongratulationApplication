using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_CongratulationApplication.Migrations
{
    public partial class ChangeFieldInPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Users_UserId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Users_UserId",
                table: "People",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Users_UserId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Users_UserId",
                table: "People",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
