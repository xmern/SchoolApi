using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SchoolApi.Authentication
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration _configuration) : IdentityDbContext<AppUser>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            //Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
        }

    }
}
