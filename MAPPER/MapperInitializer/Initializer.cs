using AppointmentSchedulerpjt.MAPPER.MapperRequest;
using AppointmentSchedulerpjt.Model;
using AutoMapper;

namespace AppointmentSchedulerpjt.MAPPER.MapperInitializer
{
    public class Initializer : Profile
    {
        public Initializer()
        {
            CreateMap<RegistrationInfo, RegistrationDTO>().ReverseMap();
            CreateMap<RegistrationInfo, GetUserinfoDTO>().ReverseMap();
            CreateMap<RegistrationInfo, GetAllUserinfoDTO>().ReverseMap();
            CreateMap<RegistrationInfo, updateUserDTO>().ReverseMap();
            CreateMap<RegistrationInfo, StaffRegistrationDTO>().ReverseMap();
            CreateMap<RegistrationInfo, StaffUpdateDTO>().ReverseMap();
            CreateMap<BookAppointment, BookAppointmentDTO>().ReverseMap();
            CreateMap<BookAppointment, GetAllAppointment>().ReverseMap();
            CreateMap<BookAppointment, ResponseAppointmentDTO>().ReverseMap();
            CreateMap<BookAppointment, EditUserAppointment>().ReverseMap(); 
           
        }
    }
}
