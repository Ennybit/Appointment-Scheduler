using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class foreihnkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment");

            migrationBuilder.DropTable(
                name: "servicesOffered");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationInfoId",
                table: "BookAppointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment",
                column: "RegistrationInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationInfoId",
                table: "BookAppointment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "servicesOffered",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegistrationInfoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicesOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_servicesOffered_AspNetUsers_RegistrationInfoId",
                        column: x => x.RegistrationInfoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "servicesOffered",
                columns: new[] { "Id", "RegistrationInfoId", "Service" },
                values: new object[,]
                {
                    { "1aff6cd2-9664-450c-880e-9216212613c1", null, "Electrician" },
                    { "62464187-c6a6-4436-9f16-5cedbdfa914e", null, "Dental" },
                    { "77dbe0d5-3b4c-40bf-bde6-43264d6ad724", null, "salon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_servicesOffered_RegistrationInfoId",
                table: "servicesOffered",
                column: "RegistrationInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment",
                column: "RegistrationInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
