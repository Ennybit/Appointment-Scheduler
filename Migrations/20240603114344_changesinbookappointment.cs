using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class changesinbookappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationInfoId",
                table: "BookAppointment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment",
                column: "RegistrationInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationInfoId",
                table: "BookAppointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAppointment_AspNetUsers_RegistrationInfoId",
                table: "BookAppointment",
                column: "RegistrationInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
