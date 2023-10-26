using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIsActiveFromEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Formulas");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Image",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Formulas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Equipments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
