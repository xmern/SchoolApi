using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolApi.Models;

namespace SchoolApi.ContextClasses
{
    public class SchoolDbContext(DbContextOptions<SchoolDbContext> options, IConfiguration _configuration) : DbContext(options)
    {
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
        public DbSet<ResultSheet> ResultSheets => Set<ResultSheet>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<SubjectRecord> SubjectRecords => Set<SubjectRecord>();
        public DbSet<StudentClassRecord> studentClassRecords => Set<StudentClassRecord>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            
        }
    }
}
