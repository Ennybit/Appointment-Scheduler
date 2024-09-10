using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
