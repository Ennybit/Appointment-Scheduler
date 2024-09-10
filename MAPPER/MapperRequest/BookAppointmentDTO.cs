using AppointmentSchedulerpjt.Model;

namespace AppointmentSchedulerpjt.MAPPER.MapperRequest
{
    public class BookAppointmentDTO
    {
        
        public string AppointmentService { get; set; }
        public string RegistrationInfoId { get; set; }
    }
  
    public class GetAllAppointment :ResponseAppointmentDTO
    {
        public string BookingDate { get; set; }
        public string AppointmentService { get; set; }
        
    }
    public class ResponseAppointmentDTO
    {
        public string worker { get; set; }
        public DateTime? AppointmentDay { get; set; }
        public string Status { get; set; }

    }

    public class EditUserAppointment
    {
        public string AppointmentService { get; set; }
    }
}
