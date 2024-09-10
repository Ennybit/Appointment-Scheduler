using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class addedautomapperandregistrationcontroler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65156f3c-65fe-4c5e-a929-c1c28799c041", "65156f3c-65fe-4c5e-a929-c1c28799c041", "WriterRole", "WRITERROLE" },
                    { "efbd3634-a8d5-44f5-9fe6-6dd642523f46", "efbd3634-a8d5-44f5-9fe6-6dd642523f46", "ReaderRole", "READERROLE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65156f3c-65fe-4c5e-a929-c1c28799c041");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efbd3634-a8d5-44f5-9fe6-6dd642523f46");
        }
    }
}
