using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class contenidoCatalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContenidoCatalogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Opcional = table.Column<string>(type: "text", nullable: false),
                    CatalogoId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenidoCatalogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContenidoCatalogos_Catalogos_CatalogoId",
                        column: x => x.CatalogoId,
                        principalTable: "Catalogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContenidoCatalogos",
                columns: new[] { "Id", "Active", "CatalogoId", "Created", "CreatedBy", "Descripcion", "LastModified", "LastModifiedBy", "Nombre", "Opcional" },
                values: new object[,]
                {
                    { 1, true, 1, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Formulario de login", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Login", "" },
                    { 2, true, 1, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Formulario de registro", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Registro", "" },
                    { 3, true, 2, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pantalla de login", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "loginscreen", "" },
                    { 4, true, 2, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pantalla de registar", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "register", "" },
                    { 5, true, 2, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pantalla de inicio", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Dashboard", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContenidoCatalogos_CatalogoId",
                table: "ContenidoCatalogos",
                column: "CatalogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContenidoCatalogos");
        }
    }
}
