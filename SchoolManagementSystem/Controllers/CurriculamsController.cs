using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.ViewModel;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurriculamsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CurriculamsController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCurriculum")]
        public async Task<ActionResult<IEnumerable<Curriculum>>> GetCurriculum()
        {
            return await _context.Curriculum.Include(c => c.Subjects).Include(c => c.Campuses).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurruculumVM>> GetCurruculum(int id)
        {
            Curriculum curriculum = await _context.Curriculum.FindAsync(id);


            CurruculumVM curruculumVM = new CurruculumVM()
            {
                CurriculumId = curriculum.CurriculumId,
                CurriculumName = curriculum.CurriculumName,
                Description = curriculum.Description
            };
            return curruculumVM;
        }


        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Curriculum>> UpdateCurriculum([FromForm] CurruculumVM vm)
        {

            Curriculum curriculum = _context.Curriculum.Find(vm.CurriculumId);
            curriculum.CurriculumName = vm.CurriculumName;
            curriculum.Description = vm.Description;

            _context.Entry(curriculum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(curriculum);
        }
        [HttpPost]
        public async Task<ActionResult<Curriculum>> PostCurriculum([FromForm] CurruculumVM vm)
        {

            Curriculum curriculum = new Curriculum()
            {
                CurriculumName = vm.CurriculumName,
                Description = vm.Description,

            };
            _context.Curriculum.Add(curriculum);
            await _context.SaveChangesAsync();
            return Ok(curriculum);
        }


        // DELETE: api/Curriculams/5
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteCurriculum(int id)
        {
            var curriculum = await _context.Curriculum.FindAsync(id);
            if (curriculum == null)
            {
                return NotFound();
            }

            _context.Curriculum.Remove(curriculum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurriculumExists(int id)
        {
            return _context.Curriculum.Any(e => e.CurriculumId == id);
        }
    }
}