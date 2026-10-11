using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UniqueISBNMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                comment: "ISBN",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book_ISBN",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "text",
                nullable: false,
                comment: "ISBN",
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25,
                oldComment: "ISBN");
        }
    }
}
