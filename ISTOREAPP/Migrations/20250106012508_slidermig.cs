using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISTOREAPP.Migrations
{
    /// <inheritdoc />
    public partial class slidermig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SliderImgId",
                table: "Sliders",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "SliderImgName",
                table: "Sliders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SliderImg",
                table: "Sliders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sliders",
                newName: "SliderImgId");

            migrationBuilder.AlterColumn<string>(
                name: "SliderImgName",
                table: "Sliders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "SliderImg",
                table: "Sliders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
