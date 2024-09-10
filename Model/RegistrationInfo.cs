using Microsoft.AspNetCore.Identity;

namespace AppointmentSchedulerpjt.Model
{
    public class RegistrationInfo : IdentityUser
    {
        public string? Department { get; set; }
        public List<BookAppointment> Appointments { get; set; }
    }
}
