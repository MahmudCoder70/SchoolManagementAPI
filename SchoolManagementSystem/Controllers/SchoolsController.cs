using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.ViewModel;
using School = SchoolManagementSystem.Models.School;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public SchoolsController(SchoolDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        //[Route("GetSchools")] 
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            return await _context.Schools.Include(c => c.Buildings).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolVM>> GetSchool(int id)
        {
            School school = await _context.Schools.FindAsync(id);


            SchoolVM schoolVM = new SchoolVM()
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                SchoolLocation = school.SchoolLocation,
                Email = school.Email,
                SchoolTypeId = school.SchoolTypeId
            };
            return schoolVM;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<School>> UpdateSchool([FromForm] SchoolVM vm)
        {


            School school = _context.Schools.Find(vm.SchoolId);
            school.SchoolName = vm.SchoolName;
            school.SchoolLocation = vm.SchoolLocation;
            school.Email = vm.Email;
            school.SchoolTypeId = vm.SchoolTypeId;

            _context.Entry(school).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(school);
        }

        [HttpPost]
        public async Task<ActionResult<School>> PostSchool([FromForm] SchoolVM vm)
        {


            School school = new School()
            {
                SchoolName = vm.SchoolName,
                SchoolLocation = vm.SchoolLocation,
                Email = vm.Email,
                SchoolTypeId = vm.SchoolTypeId

            };
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return Ok(school);
        }

        // DELETE: api/Schools/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolId == id);
        }
    }
}