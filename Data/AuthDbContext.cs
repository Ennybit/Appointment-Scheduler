using AppointmentSchedulerpjt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSchedulerpjt.Data
{
    public class AuthDbContext : IdentityDbContext<RegistrationInfo>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)   
        {
                
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "65156f3c-65fe-4c5e-a929-c1c28799c041",
                    Name = "WriterRole",
                    NormalizedName = "WriterRole".ToUpper(),
                    ConcurrencyStamp = "65156f3c-65fe-4c5e-a929-c1c28799c041"
                },
                new IdentityRole
                {
                    Id = "efbd3634-a8d5-44f5-9fe6-6dd642523f46",
                    Name = "ReaderRole",
                    NormalizedName = "ReaderRole".ToUpper(),
                    ConcurrencyStamp = "efbd3634-a8d5-44f5-9fe6-6dd642523f46"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
        public DbSet<BookAppointment> BookAppointment { get; set; }
    }
}
