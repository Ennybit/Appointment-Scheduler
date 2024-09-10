using AppointmentSchedulerpjt.Model;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class StaffRegistrationDTO : RegistrationDTO
    {
        public string Department { get; set; }

    }
    public class StaffUpdateDTO : GetUserinfoDTO
    {
        public string Department { get; set; }
    }
}
