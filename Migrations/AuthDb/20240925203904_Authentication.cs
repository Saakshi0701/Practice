using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Authentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41c59ec5-0b98-4968-8be0-b4ab583c3f68",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "225cfd05-9f75-4b04-b3f6-3c930a957af5", "AQAAAAIAAYagAAAAEAjSOzuucPF+pP5IYCrnFp+E2Eaom3UgAbTQYcI0ocPhSK36BrW4jzdBIrp0CEXIRw==", "504d366e-fece-4dd7-b9d2-d8da6ba9316e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41c59ec5-0b98-4968-8be0-b4ab583c3f68",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb29849a-0bc1-45b6-b87e-14f86b24cbe1", "AQAAAAIAAYagAAAAEODcuOJ+F7/6JdupDkEcGTgShHNAFZm3tGm08is862K+eC/1DETWfPHSrG0U8GHxHQ==", "48c97a7a-6c90-4e83-b1d3-aa7f778ccea8" });
        }
    }
}
