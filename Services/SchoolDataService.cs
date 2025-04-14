using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.ContextClasses;
using SchoolApi.Models;

namespace SchoolApi.Services
{
    public class SchoolDataService
    {
        private readonly SchoolDbContext _context;
        public SchoolDataService(SchoolDbContext context) {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
        {
            return await _context.Classes.ToListAsync();
        }
        public async Task CreateClass(Class clas){
            _context.Classes.Add(clas);
            await _context.SaveChangesAsync();
            //return clas;
        }
    }
}
