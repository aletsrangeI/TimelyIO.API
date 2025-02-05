using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class formfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Placeholder = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Validations = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<string>(type: "text", nullable: true),
                    FormularioId = table.Column<int>(type: "integer", nullable: false),
                    CatalogoId = table.Column<int>(type: "integer", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFields_Catalogos_CatalogoId",
                        column: x => x.CatalogoId,
                        principalTable: "Catalogos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormFields_ContenidoCatalogos_FormularioId",
                        column: x => x.FormularioId,
                        principalTable: "ContenidoCatalogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CatalogoId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CatalogoId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CatalogoId",
                value: 0);

            migrationBuilder.InsertData(
                table: "FormFields",
                columns: new[] { "Id", "Active", "CatalogoId", "Created", "CreatedBy", "FormularioId", "Label", "LastModified", "LastModifiedBy", "Name", "Options", "Order", "Placeholder", "Type", "Validations", "Value" },
                values: new object[,]
                {
                    { 1, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 1, "Email/Usuario", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "email", "[]", 0, "Email/Usuario", "text", "[]", " " },
                    { 2, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 1, "Password", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "password", "[]", 1, "Password", "password", "[]", " " },
                    { 3, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "Email/Usuario", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "email", "[]", 0, "Email/Usuario", "text", "[]", " " },
                    { 4, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "Password", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "password", "[]", 1, "Password", "password", "[]", " " },
                    { 5, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "Confirmar Password", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "confirmPassword", "[]", 2, "Confirmar Password", "password", "[]", " " },
                    { 6, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "Nombre", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "nombre", "[]", 3, "Nombre", "text", "[]", " " },
                    { 7, true, 0, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "Apellido", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "apellido", "[]", 4, "Apellido", "text", "[]", " " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_CatalogoId",
                table: "FormFields",
                column: "CatalogoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_FormularioId",
                table: "FormFields",
                column: "FormularioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormFields");

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CatalogoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CatalogoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ContenidoCatalogos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CatalogoId",
                value: 2);
        }
    }
}
