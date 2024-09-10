using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class appointmentday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "BookAppointment");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "BookAppointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDay",
                table: "BookAppointment",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDay",
                table: "BookAppointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "BookAppointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AppointmentTime",
                table: "BookAppointment",
                type: "time",
                nullable: true);
        }
    }
}
