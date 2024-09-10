using AppointmentSchedulerpjt.Model;

namespace AppointmentSchedulerpjt.Repo.Irepository
{
    public interface ITokenRepo
    {
        string CreateJWTToken(RegistrationInfo reginfo, List<string> roles);
    }
}
