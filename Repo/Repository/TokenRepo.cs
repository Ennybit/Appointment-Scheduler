using AppointmentSchedulerpjt.Model;
using AppointmentSchedulerpjt.Repo.Irepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentSchedulerpjt.Repo.Repository
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration configuration;

        public TokenRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(RegistrationInfo reginfo, List<string> roles)
        {
            //Create Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, reginfo.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["jwt:issuer"],
                configuration["jwt:audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
