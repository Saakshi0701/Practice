using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Auths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c78341f-aa60-4168-95b2-a655d85a4235",
                column: "Name",
                value: "Writer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41c59ec5-0b98-4968-8be0-b4ab583c3f68",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63129f62-8e3a-4255-b32d-51848eb1a28d", "AQAAAAIAAYagAAAAEDfsvKXDkFnjVmFB1nh4BUe/risYpLuoeymYaGU93O48ahcksEGuYCOo8cHsPsW6Kg==", "52b5c5b3-fed6-4483-a5d1-8daccdce6433" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c78341f-aa60-4168-95b2-a655d85a4235",
                column: "Name",
                value: "writer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41c59ec5-0b98-4968-8be0-b4ab583c3f68",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "225cfd05-9f75-4b04-b3f6-3c930a957af5", "AQAAAAIAAYagAAAAEAjSOzuucPF+pP5IYCrnFp+E2Eaom3UgAbTQYcI0ocPhSK36BrW4jzdBIrp0CEXIRw==", "504d366e-fece-4dd7-b9d2-d8da6ba9316e" });
        }
    }
}
