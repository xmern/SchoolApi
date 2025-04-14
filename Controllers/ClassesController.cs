using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.ContextClasses;
using SchoolApi.Models;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly SchoolDataService _schoolDataService;
        private readonly SchoolDbContext _context;
        public ClassesController(SchoolDataService dataService, SchoolDbContext context) { 
            _schoolDataService = dataService;
            _context = context;
        }
        [HttpGet("Get-all-classes")]
        public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
        {
            if (_context.Classes == null)
            {
                return NotFound();
            }
            return await _schoolDataService.GetAllClasses();

        }
        [HttpPost("create-class")]
        public async Task<ActionResult<Class>> CreateClass(CreateClassDto classDto)
        {
            if (_context.Classes == null)
            {
                return Problem("Entity set 'Classes' is null.");
            }

            var classEntity = new Class
            {
                Id = Guid.NewGuid(), // Let the server assign the ID
                Name = classDto.Name
            };

            await _schoolDataService.CreateClass(classEntity);

            return CreatedAtAction(nameof(GetAllClasses), new { id = classEntity.Id }, classEntity);
        }

    }
}
