using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class ForgetPasswordDTO
    {
        [Required ]
        public string Email { get; set; }
        [Required ] 
        public string ClientUrl { get; set; }
    }
}
