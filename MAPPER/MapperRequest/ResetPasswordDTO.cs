
using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string token { get; set; }
        public string Email { get; set; }
    }
}
