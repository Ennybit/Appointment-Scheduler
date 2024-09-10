using AppointmentSchedulerpjt.Data;
using AppointmentSchedulerpjt.MAPPER.MapperRequest;
using AppointmentSchedulerpjt.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppointmentSchedulerpjt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper map;
        private readonly ILogger<AppointmentController> logger;
        private readonly AuthDbContext context;
        

        public AppointmentController(IMapper map, ILogger<AppointmentController> logger, AuthDbContext context)
        {
            this.map = map;
            this.logger = logger;
            this.context = context;
        }


        [HttpPost("User/BookAppointments")]
        [Authorize(Roles = "ReaderRole")]
        public async Task<IActionResult> BookAppointment(BookAppointmentDTO bookAppointmentdto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var comp = map.Map<BookAppointment>(bookAppointmentdto);
                var result = await context.BookAppointment.AddAsync(comp);
                var checkid = await context.Users.FindAsync(bookAppointmentdto.RegistrationInfoId);

                if (checkid == null) 
                {
                    return BadRequest(new Response { Status = "Failed", Message = "User id cannot be found" });
                }

                
                await context.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "AppointMent Booked Successfully"});
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(BookAppointment)}");
                return Problem($"something went wrong in the {nameof(BookAppointment)}", statusCode: 500);
            }
        }

        [HttpGet("UserAppointment/GetAll/id")]
        [Authorize(Roles = "ReaderRole")]
        public Task<IActionResult> UserAppointmentList(string id)
        {
            if(!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            try
            {
                DateTime checktime = DateTime.Today;
                var connect = context.BookAppointment.Where(c => c.RegistrationInfoId == id && (c.Status.ToLower() == "pending" ||
                (c.Status.ToLower() == "approved" && c.AppointmentDay > checktime)));
                var result = map.Map<List<GetAllAppointment>>(connect);
                return Task.FromResult<IActionResult>(Ok(result));

            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(UserAppointmentList)}");
                return Task.FromResult<IActionResult>(Problem($"something went wrong in the {nameof(UserAppointmentList)}", statusCode: 500));
            }
        }
        [HttpGet("AppointmentHistory")]
        [Authorize(Roles = "ReaderRole")]
        public Task<IActionResult> AppointmentHistory(string id)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            try
            {
                DateTime checktime = DateTime.Today;
                var connect = context.BookAppointment.Where(c => c.RegistrationInfoId == id && (c.Status.ToLower() != "pending" ||
                (c.Status.ToLower() == "approved" && checktime > c.AppointmentDay )));
                var result = map.Map<List<GetAllAppointment>>(connect);
                return Task.FromResult<IActionResult>(Ok(result));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(AppointmentHistory)}");
                return Task.FromResult<IActionResult>(Problem($"something went wrong in the {nameof(AppointmentHistory)}", statusCode: 500));
            }


        }



        [HttpPut("UserAppointment/Edit/id")]
        [Authorize(Roles = "ReaderRole")]
        public async Task<IActionResult> EditUserAppointment(Guid id, EditUserAppointment response) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               var check = await context.BookAppointment.FindAsync(id);
                if (check.Id != id)
                {
                    return BadRequest(new Response { Status = "failed" });
                   
                }

                if(check.Status == "Pending")
                {
                    map.Map(response, check);
                    context.Entry(check).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    return Ok(new Response { Status = "success" });
                }
               return BadRequest(new Response { Status = "Failed", Message = "Appointment can't be edited"});

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(EditUserAppointment)}");
                return Problem($"something went wrong in the {nameof(EditUserAppointment)}", statusCode: 500);
            }
        }

        [HttpDelete("UserAppointment/Delete/id")]
        [Authorize(Roles = "ReaderRole")]
        public async Task<IActionResult> DeleteUserAppointment(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.BookAppointment.FindAsync(id);
               
                var result =  context.BookAppointment.Remove(check);
                
                await context.SaveChangesAsync();

                return Ok(new Response { Status = "success" });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(DeleteUserAppointment)}");
                return Problem($"something went wrong in the {nameof(DeleteUserAppointment)}", statusCode: 500);
            }
        }
    }
}
