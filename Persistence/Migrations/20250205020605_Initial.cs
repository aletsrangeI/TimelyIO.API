using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Catalogos",
                columns: new[] { "Id", "Active", "Created", "CreatedBy", "Descripcion", "LastModified", "LastModifiedBy", "Nombre" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 2, 4, 20, 6, 4, 997, DateTimeKind.Utc).AddTicks(8672), "System", "Catalogo que identifica los roles de la aplicacion", new DateTime(2025, 2, 4, 20, 6, 4, 997, DateTimeKind.Utc).AddTicks(9559), "System", "Roles" },
                    { 2, true, new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(161), "System", "Catalogo que identifica las rutas de la aplicacion", new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(165), "System", "Rutas" },
                    { 3, true, new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(167), "System", "Catalogo que identifica los formularios de la aplicacion", new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(169), "System", "Formularios" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogos");
        }
    }
}
