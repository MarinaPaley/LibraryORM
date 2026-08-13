using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class IllustratorQualityAddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "integer",
                nullable: false,
                comment: "Год издания",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "IllustratorId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConvolutus",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Сшита из нескольких журналов");

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Качество печати");

            migrationBuilder.CreateTable(
                name: "Illustrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illustrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Illustrators_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Художники");

            migrationBuilder.CreateIndex(
                name: "IX_Books_IllustratorId",
                table: "Books",
                column: "IllustratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Illustrators_PersonId",
                table: "Illustrators",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Illustrators_IllustratorId",
                table: "Books",
                column: "IllustratorId",
                principalTable: "Illustrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Illustrators_IllustratorId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Illustrators");

            migrationBuilder.DropIndex(
                name: "IX_Books_IllustratorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IllustratorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsConvolutus",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Books",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Год издания");
        }
    }
}
