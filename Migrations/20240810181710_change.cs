﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "BookAppointment",
                newName: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "BookAppointment",
                newName: "AppointmentTime");
        }
    }
}
