using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixAddressFieldsMissingBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalFormulaPricePerDay",
                table: "Quotation",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,30)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerDay",
                table: "Formula",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,30)",
                oldPrecision: 2);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalFormulaPricePerDay",
                table: "Quotation",
                type: "decimal(2,30)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerDay",
                table: "Formula",
                type: "decimal(2,30)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2)",
                oldPrecision: 2);
        }
    }
}
