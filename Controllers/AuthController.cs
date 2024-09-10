using AppointmentSchedulerpjt.Data;
using AppointmentSchedulerpjt.MAPPER.MapperRequest;
using AppointmentSchedulerpjt.Model;
using AppointmentSchedulerpjt.Repo.Irepository;
using AppointmentSchedulerpjt.Services;
using AutoMapper;
using AutoMapper.Internal;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.WebUtilities;

namespace AppointmentSchedulerpjt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<RegistrationInfo> _userManager;
        private readonly IMapper _map;
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenRepo tokenRepo;
        private readonly AuthDbContext context;
        private readonly IEmailService _emailservice;

        public AuthController(UserManager<RegistrationInfo> userManager, IMapper mapper, ILogger<AuthController> logger, ITokenRepo tokenRepo, AuthDbContext context,
            IEmailService emailService)
        {
            _userManager = userManager;
            _map = mapper;
            _logger = logger;
            this.tokenRepo = tokenRepo;
            this.context = context;
            _emailservice = emailService;
        }

        [HttpPost]
        [Route("{Role}/Registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO, [FromRoute] string Role)  
        {
            _logger.LogInformation($"Registration attempt for {registrationDTO.UserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Role.Equals("ReaderRole", StringComparison.CurrentCultureIgnoreCase))
                {
                    //.............mapping DTO to Model class
                    var identityuser = _map.Map<RegistrationInfo>(registrationDTO);

                    //.............User Registration attempt
                    var identityresult = await _userManager.CreateAsync(identityuser, registrationDTO.Password);

                    //.............returns error if user is not created successfully
                    if (!identityresult.Succeeded)
                    {
                        foreach (var item in identityresult.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }

                        return BadRequest(ModelState);

                    }
                    //.............Add Role
                    if (registrationDTO.Roles != null && registrationDTO.Roles.Contains(Role))
                    {
                        identityresult = await _userManager.AddToRolesAsync(identityuser, registrationDTO.Roles);
                    }
                    else
                    {
                        return BadRequest(new Response { Status = "failed", Message = $"invalid role {Role}" });
                    }
                    // Add Token to verify Email....
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityuser);
                    var confirmationurl = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = identityuser.Email }, Request.Scheme);
                    var mailrequest = new MailRequest() { ToEmail = identityuser.Email, Subject = "Email Confirmation", Body = confirmationurl };
                    await _emailservice.SendEmailAsync(mailrequest);

                    return Ok(new Response { Status = "success", Message = "User created successfully, Please confirm your email" });

                }
                return BadRequest(new Response { Status = "Failed", Message = "Invalid Role" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }

        }

        [HttpPost]
        [Route("{Role}/StaffRegistration")]
        public async Task<IActionResult> StaffRegister([FromBody] StaffRegistrationDTO registrationDTO, [FromRoute] string Role)
        {
            _logger.LogInformation($"Registration attempt for {registrationDTO.UserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Role.Equals("WriterRole", StringComparison.CurrentCultureIgnoreCase))
                {
                    //.............mapping DTO to Model class
                    var identityuser = _map.Map<RegistrationInfo>(registrationDTO);
                    //.............User Registration attempt
                    var identityresult = await _userManager.CreateAsync(identityuser, registrationDTO.Password);

                    //.............returns error if user is not created successfully
                    if (!identityresult.Succeeded)
                    {
                        foreach (var item in identityresult.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }

                        return BadRequest(ModelState);

                    }
                    if (registrationDTO.Roles != null && registrationDTO.Roles.Contains(Role))
                    {
                        identityresult = await _userManager.AddToRolesAsync(identityuser, registrationDTO.Roles);
                    }
                    else
                    {
                        return BadRequest(new Response { Status = "failed", Message = $"invalid role {Role}" });
                    }
                    // Add Token to verify Email....
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityuser);
                    var confirmationurl = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = identityuser.Email }, Request.Scheme);
                    var mailrequest = new MailRequest() { ToEmail = identityuser.Email, Subject = "Email Confirmation", Body = confirmationurl };
                    await _emailservice.SendEmailAsync(mailrequest);

                    return Ok(new Response { Status = "success", Message = "User created successfully, Please confirm your email" });
                }
                return BadRequest(new Response { Status = "Failed", Message = "Invalid Role" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(StaffRegister)}");
                return Problem($"Something went wrong in the {nameof(StaffRegister)}", statusCode: 500);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login attempt for {loginDTO.UserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null)
                {
                    var checkpassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (checkpassword)
                    {
                        //get roles
                        var userrole = await _userManager.GetRolesAsync(user);
                        //create token
                        if (userrole != null)
                        {
                            var jwttoken = tokenRepo.CreateJWTToken(user, userrole.ToList());
                            return Ok(new Response { Message = jwttoken, Status = "success" });
                        }
                    }
                }

                return Unauthorized(new Response { Message = "Username or Password incorrect", Status = "Failed" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO forgetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);
            if (user == null)
            {
                return BadRequest("invali request");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"email", forgetPassword.Email},
                {"token", token}
            };
            var callback = QueryHelpers.AddQueryString(forgetPassword.ClientUrl, param);
            var messages = new MailRequest { Body = token, Subject = "ResetLink", ToEmail = forgetPassword.Email };
            await _emailservice.SendEmailAsync(messages);
            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return BadRequest("invalid request");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.token, resetPassword.Password);
            if(!result.Succeeded)
            {
                var error = result.Errors.Select(c => c.Description);
                return BadRequest(new { Errors = error });
            }
            return Ok();
        }
        [HttpPost("ChangePassword")]
        [Authorize(Roles = "ReaderRole,WtiterRole")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getuser = await _userManager.FindByEmailAsync(changePassword.Email);
            if (getuser == null)
            {
                return BadRequest("invalid request");
            }
            var checkpass = await _userManager.CheckPasswordAsync(getuser, changePassword.OldPassword);
            if(!checkpass)
            {
                return BadRequest("invalid request");
            }
            await _userManager.ChangePasswordAsync(getuser, changePassword.OldPassword, changePassword.NewPassword);
            
            return Ok(new Response { Status = "Success", Message = "Password change succesfull"});
        }


        [HttpPost]
        [Route("SendMail")]
        public async Task<IActionResult> SendMail()
        {
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "jaida94@ethereal.email";
                mailRequest.Subject = " TESTTTTTTTTTTTTTTTTT";
                mailRequest.Body = "THANK YOU FOR SUBSCRIBING";
                await _emailservice.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ;
            }

        }
        
        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "success", Message = "Email Confirmed Successfully"});
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "erroe", Message = "internal error" });
        }

    }
}
