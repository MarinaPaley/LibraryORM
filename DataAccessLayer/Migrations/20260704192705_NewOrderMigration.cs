using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "Streets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SeriaName",
                table: "Serias",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "Rooms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PublisherName",
                table: "Publishers",
                newName: "OriginTitle");

            migrationBuilder.RenameColumn(
                name: "PublisheOriginName",
                table: "Publishers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ManuscriptTitle",
                table: "Manuscripts",
                newName: "OriginTitle");

            migrationBuilder.RenameColumn(
                name: "ManuscriptOriginTitle",
                table: "Manuscripts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "Languages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Cities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CabinetName",
                table: "Cabinets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BookTypeName",
                table: "BookTypes",
                newName: "Name");

            migrationBuilder.AlterTable(
                name: "Items",
                comment: "Экземпляры книг");

            migrationBuilder.AlterTable(
                name: "Editors",
                comment: "Редакторы");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Streets",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                comment: "Назание улицы",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название улицы");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shelves",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                comment: "Название полки",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Название полки");

            migrationBuilder.AlterColumn<string>(
                name: "OriginTitle",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название издательства",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название издательства");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "Название издательства",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Оригинальное название издательства");

            migrationBuilder.AlterColumn<string>(
                name: "OriginTitle",
                table: "Manuscripts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название произведения",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название произведения");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Manuscripts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "Название произведения",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Оригинальное название произведения");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                comment: "Назване города",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название города");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_OriginTitle",
                table: "Publishers",
                column: "OriginTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookTypes_Name",
                table: "BookTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_OriginTitle",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Languages_Name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Genres_Name",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_BookTypes_Name",
                table: "BookTypes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Streets",
                newName: "StreetName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Serias",
                newName: "SeriaName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Rooms",
                newName: "RoomName");

            migrationBuilder.RenameColumn(
                name: "OriginTitle",
                table: "Publishers",
                newName: "PublisherName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Publishers",
                newName: "PublisheOriginName");

            migrationBuilder.RenameColumn(
                name: "OriginTitle",
                table: "Manuscripts",
                newName: "ManuscriptTitle");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Manuscripts",
                newName: "ManuscriptOriginTitle");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Languages",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "CityName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cabinets",
                newName: "CabinetName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BookTypes",
                newName: "BookTypeName");

            migrationBuilder.AlterTable(
                name: "Items",
                oldComment: "Экземпляры книг");

            migrationBuilder.AlterTable(
                name: "Editors",
                oldComment: "Редакторы");

            migrationBuilder.AlterColumn<string>(
                name: "StreetName",
                table: "Streets",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                comment: "Название улицы",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Назание улицы");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shelves",
                type: "text",
                nullable: false,
                comment: "Название полки",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название полки");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "Название издательства",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Оригинальное название издательства");

            migrationBuilder.AlterColumn<string>(
                name: "PublisheOriginName",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название издательства",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название издательства");

            migrationBuilder.AlterColumn<string>(
                name: "ManuscriptTitle",
                table: "Manuscripts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "Название произведения",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "Оригинальное название произведения");

            migrationBuilder.AlterColumn<string>(
                name: "ManuscriptOriginTitle",
                table: "Manuscripts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название произведения",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Название произведения");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Cities",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                comment: "Название города",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldComment: "Назване города");
        }
    }
}
