using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class appointmentsandothers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "BookAppointment");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BookAppointment");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "BookAppointment",
                newName: "AppointmentService");

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentDate",
                table: "BookAppointment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AppointmentTime",
                table: "BookAppointment",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "worker",
                table: "BookAppointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "servicesOffered",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationInfoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servicesOffered");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "BookAppointment");

            migrationBuilder.DropColumn(
                name: "worker",
                table: "BookAppointment");

            migrationBuilder.RenameColumn(
                name: "AppointmentService",
                table: "BookAppointment",
                newName: "LastName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "BookAppointment",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "BookAppointment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BookAppointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
