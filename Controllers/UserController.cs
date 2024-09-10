using AppointmentSchedulerpjt.Data;
using AppointmentSchedulerpjt.MAPPER.MapperRequest;
using AppointmentSchedulerpjt.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AppointmentSchedulerpjt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper map;
        private readonly ILogger<UserController> logger;
        private readonly AuthDbContext context;
        public UserController(IMapper map, ILogger<UserController> logger, AuthDbContext context)
        {
            this.map = map;
            this.logger = logger;
            this.context = context;
        }
        [HttpGet("Info/Id")]
        [Authorize(Roles = "ReaderRole")]
        public async Task<IActionResult> GetUserByid(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var chcek = context.Users.Where(c => c.Id == id).FirstOrDefault();
                if (chcek == null)
                {
                    return BadRequest(ModelState);
                }
                var result = map.Map<GetAllUserinfoDTO>(chcek);
                return Ok(result);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(GetUserByid)}");
                return Problem($"something went wrong in the {nameof(GetUserByid)}", statusCode: 500);
            }
        }
        [HttpPut("InfoUpdate/id")]
        [Authorize(Roles = "ReaderRole")]
        public async Task<IActionResult> UpdateProfile(string id, updateUserDTO updateUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Users.FindAsync(id);
                if(check == null)
                {
                    return BadRequest(ModelState);
                }
                map.Map(updateUser, check);
                context.Entry(check).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "Updated Successfully"});
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(UpdateProfile)}");
                return Problem($"Somethiing went wrong in {nameof(UpdateProfile)}", statusCode: 500);
            }
        }

    }
}
