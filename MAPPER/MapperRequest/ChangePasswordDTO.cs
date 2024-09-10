using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
