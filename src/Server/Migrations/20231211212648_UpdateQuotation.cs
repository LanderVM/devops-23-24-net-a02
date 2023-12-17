using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devops2324neta02.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Opmerking",
                table: "Quotation",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
         }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.DropColumn(
              name: "Opmerking",
              table: "Quotation");
        }
    }
}
