namespace AppointmentSchedulerpjt.Model
{
    public class BookAppointment
    {
        public Guid Id { get; set; }
        public string AppointmentService { get; set; }
        public string? Worker { get; set; }
        public string? BookingDate { get; set; } = DateTime.Now.ToString("D");
        public DateTime? AppointmentDay { get; set; }
        public string Status { get; set; } = "Pending";
        public RegistrationInfo RegistrationInfo { get; set; }
        public string RegistrationInfoId { get; set; }
    }
}
