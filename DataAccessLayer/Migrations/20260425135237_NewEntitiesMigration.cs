using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewEntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IBSN",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DateDeath",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PatronicName",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Contributors");

            migrationBuilder.AddColumn<Guid>(
                name: "CabinetId",
                table: "Shelves",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: true,
                comment: "Название книги",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Название книги");

            migrationBuilder.AddColumn<string>(
                name: "Annotation",
                table: "Books",
                type: "text",
                nullable: true,
                comment: "Аннотация");

            migrationBuilder.AddColumn<Guid>(
                name: "BookTypeId",
                table: "Books",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Doi",
                table: "Books",
                type: "text",
                nullable: true,
                comment: "DOI");

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EditorId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "ISBN");

            migrationBuilder.AddColumn<Guid>(
                name: "SeriaId",
                table: "Books",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Books",
                type: "text",
                nullable: true,
                comment: "URL");

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Books",
                type: "integer",
                nullable: true,
                comment: "Том");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContributorType",
                table: "Contributors",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Contributors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contributors",
                table: "Contributors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BookTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookTypeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Тип книги")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название города")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Жанр")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Язык")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FamilyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Фамилия персоны"),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Имя персоны"),
                    PatronymicName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Отчество персоны"),
                    DateBirth = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата рождения"),
                    DateDeath = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата смерти")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                },
                comment: "Персоны: авторы, переводчики, редакторы");

            migrationBuilder.CreateTable(
                name: "Serias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriaName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Серия")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StreetName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название улицы"),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manuscripts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ManuscriptTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название произведения"),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата начала написания"),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: true, comment: "Дата окончания написания"),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manuscripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manuscripts_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Рукописи произведений");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    StreetId = table.Column<Guid>(type: "uuid", nullable: false),
                    House = table.Column<int>(type: "integer", nullable: false, comment: "Номер дома"),
                    BuildingSuffix = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "Корпус или владение"),
                    Apartment = table.Column<int>(type: "integer", nullable: true, comment: "Номер квартиры")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthorManuscript",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorManuscript", x => new { x.AuthorsId, x.ManuscriptsId });
                    table.ForeignKey(
                        name: "FK_AuthorManuscript_Contributors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorManuscript_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookManuscript",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookManuscript", x => new { x.BooksId, x.ManuscriptsId });
                    table.ForeignKey(
                        name: "FK_BookManuscript_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookManuscript_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreManuscript",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreManuscript", x => new { x.GenresId, x.ManuscriptsId });
                    table.ForeignKey(
                        name: "FK_GenreManuscript_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreManuscript_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManuscriptReviewer",
                columns: table => new
                {
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManuscriptReviewer", x => new { x.ManuscriptsId, x.ReviewersId });
                    table.ForeignKey(
                        name: "FK_ManuscriptReviewer_Contributors_ReviewersId",
                        column: x => x.ReviewersId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManuscriptReviewer_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManuscriptTranslator",
                columns: table => new
                {
                    ManuscriptsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TranslatorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManuscriptTranslator", x => new { x.ManuscriptsId, x.TranslatorsId });
                    table.ForeignKey(
                        name: "FK_ManuscriptTranslator_Contributors_TranslatorsId",
                        column: x => x.TranslatorsId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManuscriptTranslator_Manuscripts_ManuscriptsId",
                        column: x => x.ManuscriptsId,
                        principalTable: "Manuscripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PublisherName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название издательства"),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publishers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название комнаты")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Комнаты в зданиях");

            migrationBuilder.CreateTable(
                name: "BookPublisher",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPublisher", x => new { x.BooksId, x.PublishersId });
                    table.ForeignKey(
                        name: "FK_BookPublisher_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPublisher_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true),
                    CabinetName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название шкафа")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabinets_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Шкафы в комнатах");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_CabinetId",
                table: "Shelves",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookTypeId",
                table: "Books",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_EditorId",
                table: "Books",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SeriaId",
                table: "Books",
                column: "SeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_PersonId",
                table: "Contributors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorManuscript_ManuscriptsId",
                table: "AuthorManuscript",
                column: "ManuscriptsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookManuscript_ManuscriptsId",
                table: "BookManuscript",
                column: "ManuscriptsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPublisher_PublishersId",
                table: "BookPublisher",
                column: "PublishersId");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_BookTypeName",
                table: "BookTypes",
                column: "BookTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabinet_Name",
                table: "Cabinets",
                column: "CabinetName");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_RoomId",
                table: "Cabinets",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "Cities",
                column: "CityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenreManuscript_ManuscriptsId",
                table: "GenreManuscript",
                column: "ManuscriptsId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                table: "Genres",
                column: "GenreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_Name",
                table: "Languages",
                column: "LanguageName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManuscriptReviewer_ReviewersId",
                table: "ManuscriptReviewer",
                column: "ReviewersId");

            migrationBuilder.CreateIndex(
                name: "IX_Manuscript_Title",
                table: "Manuscripts",
                column: "ManuscriptTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Manuscripts_LanguageId",
                table: "Manuscripts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ManuscriptTranslator_TranslatorsId",
                table: "ManuscriptTranslator",
                column: "TranslatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_Name",
                table: "Publishers",
                column: "PublisherName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_AddressId",
                table: "Publishers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Name",
                table: "Rooms",
                column: "RoomName");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AddressId",
                table: "Rooms",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Seria_Name",
                table: "Serias",
                column: "SeriaName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_CityId",
                table: "Streets",
                column: "CityId");

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
                name: "FK_Contributors_Persons_PersonId",
                table: "Contributors",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Contributors_Persons_PersonId",
                table: "Contributors");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Cabinets_CabinetId",
                table: "Shelves");

            migrationBuilder.DropTable(
                name: "AuthorManuscript");

            migrationBuilder.DropTable(
                name: "BookManuscript");

            migrationBuilder.DropTable(
                name: "BookPublisher");

            migrationBuilder.DropTable(
                name: "BookTypes");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropTable(
                name: "GenreManuscript");

            migrationBuilder.DropTable(
                name: "ManuscriptReviewer");

            migrationBuilder.DropTable(
                name: "ManuscriptTranslator");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Serias");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Manuscripts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_CabinetId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookTypeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_EditorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_SeriaId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contributors",
                table: "Contributors");

            migrationBuilder.DropIndex(
                name: "IX_Contributors_PersonId",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "CabinetId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Annotation",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookTypeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Doi",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SeriaId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ContributorType",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Contributors");

            migrationBuilder.RenameTable(
                name: "Contributors",
                newName: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Название книги",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "Название книги");

            migrationBuilder.AddColumn<string>(
                name: "IBSN",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "IBSN");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateBirth",
                table: "Authors",
                type: "date",
                nullable: true,
                comment: "Дата рождения");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateDeath",
                table: "Authors",
                type: "date",
                nullable: true,
                comment: "Дата смерти");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Authors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Фамилия");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Имя");

            migrationBuilder.AddColumn<string>(
                name: "PatronicName",
                table: "Authors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                comment: "Отчество");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");
        }
    }
}
