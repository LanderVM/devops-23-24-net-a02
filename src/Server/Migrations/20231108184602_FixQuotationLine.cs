using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixQuotationLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountOrdered",
                table: "QuotationLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentOrderedId",
                table: "QuotationLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalEquipmentPrice",
                table: "QuotationLine",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateIndex(
                name: "IX_QuotationLine_EquipmentOrderedId",
                table: "QuotationLine",
                column: "EquipmentOrderedId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationLine_Equipment_EquipmentOrderedId",
                table: "QuotationLine",
                column: "EquipmentOrderedId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationLine_Equipment_EquipmentOrderedId",
                table: "QuotationLine");

            migrationBuilder.DropIndex(
                name: "IX_QuotationLine_EquipmentOrderedId",
                table: "QuotationLine");

            migrationBuilder.DropColumn(
                name: "AmountOrdered",
                table: "QuotationLine");

            migrationBuilder.DropColumn(
                name: "EquipmentOrderedId",
                table: "QuotationLine");

            migrationBuilder.DropColumn(
                name: "OriginalEquipmentPrice",
                table: "QuotationLine");

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
