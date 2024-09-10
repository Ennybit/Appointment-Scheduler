using AppointmentSchedulerpjt.Data;
using AppointmentSchedulerpjt.MAPPER.MapperRequest;
using AppointmentSchedulerpjt.Model;
using AppointmentSchedulerpjt.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSchedulerpjt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper map;
        private readonly ILogger<AdminController> logger;
        private readonly AuthDbContext context;
        private readonly IEmailService emailservice;
        public AdminController(IMapper map, ILogger<AdminController> logger, AuthDbContext context, IEmailService service)
        {
            this.map = map;
            this.context = context;
            this.emailservice = service;
            this.logger = logger;
        }

        [HttpPut("UpdateAdminInfo/id")]
        [Authorize(Roles = "WriterRole")]
        public  async Task<IActionResult> UpdateAdminInfo(string id, StaffUpdateDTO staffUpdate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Users.FindAsync(id);
                if (check == null)
                {
                    return NotFound();
                }
                if (check.Id == id)
                {
                    map.Map(staffUpdate, check);
                    context.Entry(check).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(UpdateAdminInfo)}");
                return Problem($"something went wrong in the {nameof(UpdateAdminInfo)}", statusCode: 500);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "WriterRole")]
        public async Task<IActionResult> Getall()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Users.Include(c => c.Appointments).ToListAsync();
                if (result == null)
                {
                    return BadRequest();
                }
                var mapresult = map.Map<List<GetAllUserinfoDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(Getall)}");
                return Problem($"something went wrong in the {nameof(Getall)}", statusCode: 500);
            }
        }

        [HttpGet("GetUser/id")]
        [Authorize(Roles = "WriterRole")]
        public async Task<IActionResult> Getbyid(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Users.Where(c => c.Id == id).Include(c => c.Appointments).ToListAsync();
                var mapresult = map.Map<List<GetAllUserinfoDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(Getbyid)}");
                return Problem($"something went wrong in the {nameof(Getbyid)}", statusCode: 500);
            }
        }

        [HttpGet("GetAllAppointment")]
        [Authorize(Roles = "WriterRole")]
        public async Task<IActionResult> GetAppointments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var connect = await context.BookAppointment.ToListAsync();
                var result = map.Map<List<GetAllAppointment>>(connect);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something is wrong in the {nameof(GetAppointments)}");
                return Problem($"something went wrong in the {nameof(GetAppointments)}", statusCode: 500);
            }
        }

        [HttpPut("Appointment/Response/id")]
        [Authorize(Roles = "WriterRole")]
        public async Task<IActionResult> AppointmentResponse(Guid id, ResponseAppointmentDTO response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.BookAppointment.FindAsync(id);
                if(check == null)
                {
                    return NotFound();
                }
                var connect = map.Map(response, check);
                context.Entry(check).State = EntityState.Modified;
              
                var mail = await context.Users.FindAsync(check.RegistrationInfo);
                if(mail == null)
                {
                    return NotFound(new Response { Status = "failed", Message = "Email not found" });
                }
                var mailrequest = new MailRequest { ToEmail = mail.Email, Subject = "Appointment Response", 
                    Body = $"Your Appointment as been approved and is scheduled for {check.AppointmentDay}" };
                  await context.SaveChangesAsync();
                return Ok(new Response { Status = "Success" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something is wrong in the {nameof(AppointmentResponse)}");
                return Problem($"something went wrong in the {nameof(AppointmentResponse)}", statusCode: 500);
            }
        }

    }
}
