using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolApi.Models;

namespace SchoolApi.ContextClasses
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options, IConfiguration _configuration) : DbContext(options)
    {
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            
        }
    }
}
