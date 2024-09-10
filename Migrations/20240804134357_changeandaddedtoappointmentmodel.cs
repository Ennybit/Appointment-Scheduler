using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedulerpjt.Migrations
{
    /// <inheritdoc />
    public partial class changeandaddedtoappointmentmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingDate",
                table: "BookAppointment",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "BookAppointment");
        }
    }
}
