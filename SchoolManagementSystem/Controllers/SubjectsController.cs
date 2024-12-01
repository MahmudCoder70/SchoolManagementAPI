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

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public SubjectsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectVM>> GetSubject(int id)
        {
            Subject subject = await _context.Subjects.FindAsync(id);


            SubjectVM subjectVM = new SubjectVM()
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                ClassId = subject.ClassId,
                CurriculumId = subject.CurriculumId
            };
            return subjectVM;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Subject>> UpdateSubject([FromForm] SubjectVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Subject subject = _context.Subjects.Find(vm.SubjectId);
            subject.SubjectName = vm.SubjectName;
            subject.ClassId = vm.ClassId;
            subject.CurriculumId = vm.CurriculumId;

            _context.Entry(subject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(subject);
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject([FromForm] SubjectVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Subject subjects = new Subject()
            {
                SubjectName = vm.SubjectName,
                ClassId = vm.ClassId,
                CurriculumId = vm.CurriculumId

            };
            _context.Subjects.Add(subjects);
            await _context.SaveChangesAsync();
            return Ok(subjects);
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<Subject>> DeleteSubject(int id)
        {
            Subject subject = _context.Subjects.Find(id);

            var existingSubject = _context.TeachersSubject.Where(x => x.SubjectId == subject.SubjectId).ToList();
            foreach (var item in existingSubject)
            {
                _context.TeachersSubject.Remove(item);
            }
            _context.Entry(subject).State = EntityState.Deleted;
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(subject);
        }
    }
}