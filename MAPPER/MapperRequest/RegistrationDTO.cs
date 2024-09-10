using AppointmentSchedulerpjt.Model;
using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class RegistrationDTO : LoginDTO
    {
       
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public String[] Roles { get; set; }
     

    }

    public class GetUserinfoDTO 
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }

    public class GetAllUserinfoDTO : GetUserinfoDTO
    {
        public List<ResponseAppointmentDTO> Appointments { get; set; }

    }
    public class updateUserDTO
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
