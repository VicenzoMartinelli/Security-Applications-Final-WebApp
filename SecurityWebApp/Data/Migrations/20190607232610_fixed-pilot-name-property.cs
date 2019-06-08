using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityWebApp.Data.Migrations
{
    public partial class fixedpilotnameproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shuttles_Pilots_PilotoId",
                table: "Shuttles");

            migrationBuilder.RenameColumn(
                name: "PilotoId",
                table: "Shuttles",
                newName: "PilotId");

            migrationBuilder.RenameIndex(
                name: "IX_Shuttles_PilotoId",
                table: "Shuttles",
                newName: "IX_Shuttles_PilotId");

            migrationBuilder.AlterColumn<string>(
                name: "Producer",
                table: "Shuttles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Shuttles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shuttles_Pilots_PilotId",
                table: "Shuttles",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shuttles_Pilots_PilotId",
                table: "Shuttles");

            migrationBuilder.RenameColumn(
                name: "PilotId",
                table: "Shuttles",
                newName: "PilotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Shuttles_PilotId",
                table: "Shuttles",
                newName: "IX_Shuttles_PilotoId");

            migrationBuilder.AlterColumn<string>(
                name: "Producer",
                table: "Shuttles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Shuttles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Shuttles_Pilots_PilotoId",
                table: "Shuttles",
                column: "PilotoId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
