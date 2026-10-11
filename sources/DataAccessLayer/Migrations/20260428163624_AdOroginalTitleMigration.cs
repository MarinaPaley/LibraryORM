using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AdOroginalTitleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManuscriptOriginTitle",
                table: "Manuscripts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название произведения");

            migrationBuilder.CreateIndex(
                name: "IX_Manuscript_OriginTitle",
                table: "Manuscripts",
                column: "ManuscriptOriginTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Manuscript_OriginTitle",
                table: "Manuscripts");

            migrationBuilder.DropColumn(
                name: "ManuscriptOriginTitle",
                table: "Manuscripts");
        }
    }
}
