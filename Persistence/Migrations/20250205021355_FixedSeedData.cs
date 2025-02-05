using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2025, 2, 4, 20, 6, 4, 997, DateTimeKind.Utc).AddTicks(8672), new DateTime(2025, 2, 4, 20, 6, 4, 997, DateTimeKind.Utc).AddTicks(9559) });

            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(161), new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(165) });

            migrationBuilder.UpdateData(
                table: "Catalogos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(167), new DateTime(2025, 2, 4, 20, 6, 4, 998, DateTimeKind.Utc).AddTicks(169) });
        }
    }
}
