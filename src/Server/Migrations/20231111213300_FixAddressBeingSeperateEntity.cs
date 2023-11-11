using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixAddressBeingSeperateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_BillingAddressId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Address_EventLocationId",
                table: "Quotation");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_EventLocationId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EventLocationId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "Customer");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalFormulaPricePerDay",
                table: "Quotation",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,30)",
                oldPrecision: 2);

            migrationBuilder.AddColumn<string>(
                name: "EventLocation_City",
                table: "Quotation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EventLocation_HouseNumber",
                table: "Quotation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EventLocation_PostalCode",
                table: "Quotation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EventLocation_Street",
                table: "Quotation",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerDay",
                table: "Formula",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,30)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Equipment",
                type: "decimal(2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,30)",
                oldPrecision: 2);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                table: "Customer",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_HouseNumber",
                table: "Customer",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_PostalCode",
                table: "Customer",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street",
                table: "Customer",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventLocation_City",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "EventLocation_HouseNumber",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "EventLocation_PostalCode",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "EventLocation_Street",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BillingAddress_HouseNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BillingAddress_PostalCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street",
                table: "Customer");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalFormulaPricePerDay",
                table: "Quotation",
                type: "decimal(2,30)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2)",
                oldPrecision: 2);

            migrationBuilder.AddColumn<int>(
                name: "EventLocationId",
                table: "Quotation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerDay",
                table: "Formula",
                type: "decimal(2,30)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Equipment",
                type: "decimal(2,30)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2)",
                oldPrecision: 2);

            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HouseNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_EventLocationId",
                table: "Quotation",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer",
                column: "BillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_BillingAddressId",
                table: "Customer",
                column: "BillingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Address_EventLocationId",
                table: "Quotation",
                column: "EventLocationId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
