using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewVersionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManuscript_Contributors_AuthorsId",
                table: "AuthorManuscript");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Contributors_EditorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Serias_SeriaId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shelves_ShelfId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabinets_Rooms_RoomId",
                table: "Cabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributors_Persons_PersonId",
                table: "Contributors");

            migrationBuilder.DropForeignKey(
                name: "FK_ManuscriptReviewer_Contributors_ReviewersId",
                table: "ManuscriptReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_Manuscripts_Languages_LanguageId",
                table: "Manuscripts");

            migrationBuilder.DropForeignKey(
                name: "FK_ManuscriptTranslator_Contributors_TranslatorsId",
                table: "ManuscriptTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_Name",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Seria_Name",
                table: "Serias");

            migrationBuilder.DropIndex(
                name: "IX_Room_Name",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Publisher_Name",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Manuscript_OriginTitle",
                table: "Manuscripts");

            migrationBuilder.DropIndex(
                name: "IX_Manuscript_Title",
                table: "Manuscripts");

            migrationBuilder.DropIndex(
                name: "IX_Manuscripts_LanguageId",
                table: "Manuscripts");

            migrationBuilder.DropIndex(
                name: "IX_Language_Name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Genre_Name",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_City_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cabinet_Name",
                table: "Cabinets");

            migrationBuilder.DropIndex(
                name: "IX_BookType_BookTypeName",
                table: "BookTypes");

            migrationBuilder.DropIndex(
                name: "IX_Book_ISBN",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ShelfId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contributors",
                table: "Contributors");

            migrationBuilder.DropIndex(
                name: "IX_Contributors_PersonId",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Manuscripts");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ContributorType",
                table: "Contributors");

            migrationBuilder.RenameTable(
                name: "Contributors",
                newName: "Translators");

            migrationBuilder.AlterTable(
                name: "Rooms",
                comment: "Комнаты",
                oldComment: "Комнаты в зданиях");

            migrationBuilder.AddColumn<string>(
                name: "PublisheOriginName",
                table: "Publishers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                comment: "Оригинальное название издательства");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Название книги",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Название книги");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true,
                comment: "ISBN",
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25,
                oldComment: "ISBN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translators",
                table: "Translators",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editors_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShelfId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LanguageManuscript",
                columns: table => new
                {
                    LanguagesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageManuscript", x => new { x.LanguagesId, x.ManuscriptsId });
                    table.ForeignKey(
                        name: "FK_LanguageManuscript_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageManuscript_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviwers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviwers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviwers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translators_PersonId",
                table: "Translators",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PersonId",
                table: "Authors",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editors_PersonId",
                table: "Editors",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_BookId",
                table: "Items",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShelfId",
                table: "Items",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageManuscript_ManuscriptsId",
                table: "LanguageManuscript",
                column: "ManuscriptsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviwers_PersonId",
                table: "Reviwers",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManuscript_Authors_AuthorsId",
                table: "AuthorManuscript",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books",
                column: "BookTypeId",
                principalTable: "BookTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Editors_EditorId",
                table: "Books",
                column: "EditorId",
                principalTable: "Editors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Serias_SeriaId",
                table: "Books",
                column: "SeriaId",
                principalTable: "Serias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinets_Rooms_RoomId",
                table: "Cabinets",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ManuscriptReviewer_Reviwers_ReviewersId",
                table: "ManuscriptReviewer",
                column: "ReviewersId",
                principalTable: "Reviwers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManuscriptTranslator_Translators_TranslatorsId",
                table: "ManuscriptTranslator",
                column: "TranslatorsId",
                principalTable: "Translators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Translators_Persons_PersonId",
                table: "Translators",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManuscript_Authors_AuthorsId",
                table: "AuthorManuscript");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Editors_EditorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Serias_SeriaId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Cabinets_Rooms_RoomId",
                table: "Cabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_ManuscriptReviewer_Reviwers_ReviewersId",
                table: "ManuscriptReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_ManuscriptTranslator_Translators_TranslatorsId",
                table: "ManuscriptTranslator");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves");

            migrationBuilder.DropForeignKey(
                name: "FK_Translators_Persons_PersonId",
                table: "Translators");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Editors");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "LanguageManuscript");

            migrationBuilder.DropTable(
                name: "Reviwers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translators",
                table: "Translators");

            migrationBuilder.DropIndex(
                name: "IX_Translators_PersonId",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "PublisheOriginName",
                table: "Publishers");

            migrationBuilder.RenameTable(
                name: "Translators",
                newName: "Contributors");

            migrationBuilder.AlterTable(
                name: "Rooms",
                comment: "Комнаты в зданиях",
                oldComment: "Комнаты");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "Manuscripts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: true,
                comment: "Название книги",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Название книги");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                comment: "ISBN",
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25,
                oldNullable: true,
                oldComment: "ISBN");

            migrationBuilder.AddColumn<Guid>(
                name: "ShelfId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ContributorType",
                table: "Contributors",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contributors",
                table: "Contributors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_Name",
                table: "Shelves",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seria_Name",
                table: "Serias",
                column: "SeriaName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_Name",
                table: "Rooms",
                column: "RoomName");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_Name",
                table: "Publishers",
                column: "PublisherName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manuscript_OriginTitle",
                table: "Manuscripts",
                column: "ManuscriptOriginTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Manuscript_Title",
                table: "Manuscripts",
                column: "ManuscriptTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Manuscripts_LanguageId",
                table: "Manuscripts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_Name",
                table: "Languages",
                column: "LanguageName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                table: "Genres",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "Cities",
                column: "CityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabinet_Name",
                table: "Cabinets",
                column: "CabinetName");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_BookTypeName",
                table: "BookTypes",
                column: "BookTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ShelfId",
                table: "Books",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_PersonId",
                table: "Contributors",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManuscript_Contributors_AuthorsId",
                table: "AuthorManuscript",
                column: "AuthorsId",
                principalTable: "Contributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books",
                column: "BookTypeId",
                principalTable: "BookTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Contributors_EditorId",
                table: "Books",
                column: "EditorId",
                principalTable: "Contributors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Serias_SeriaId",
                table: "Books",
                column: "SeriaId",
                principalTable: "Serias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shelves_ShelfId",
                table: "Books",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinets_Rooms_RoomId",
                table: "Cabinets",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contributors_Persons_PersonId",
                table: "Contributors",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManuscriptReviewer_Contributors_ReviewersId",
                table: "ManuscriptReviewer",
                column: "ReviewersId",
                principalTable: "Contributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manuscripts_Languages_LanguageId",
                table: "Manuscripts",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManuscriptTranslator_Contributors_TranslatorsId",
                table: "ManuscriptTranslator",
                column: "TranslatorsId",
                principalTable: "Contributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
