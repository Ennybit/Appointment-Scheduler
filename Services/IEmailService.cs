using AppointmentSchedulerpjt.Model;

namespace AppointmentSchedulerpjt.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
