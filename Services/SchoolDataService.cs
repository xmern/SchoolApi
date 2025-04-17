using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.ContextClasses;
using SchoolApi.Models;
using SchoolApi.Models.DTOs;

namespace SchoolApi.Services
{
    public class SchoolDataService
    {
        private readonly SchoolDbContext _context;
        public SchoolDataService(SchoolDbContext context) {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<GetClassesDto>>> GetAllClasses()
        {
            var classes = await _context.Classes
                    .Select(c => new GetClassesDto
                        {
                            Id = c.Id,
                            Name = c.Name
                        })
                        .ToListAsync();
            return classes;
        }
        public async Task CreateClass(Class clas){
            _context.Classes.Add(clas);
            await _context.SaveChangesAsync();
            //return clas;
        }
    }
}
