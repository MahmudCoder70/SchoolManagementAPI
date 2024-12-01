using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.DTO;
using SchoolManagementSystem.Models.ViewModel;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment _env;
        public StudentsController(SchoolDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCampusClassSection>>> GetStudents()
        {
            return await _context.StudentCampusClassSections.Include(x => x.Student).Include(x => x.Student!.Gender).Include(x=>x.Student!.Shift).Include(sccs => sccs.CampusClassSection).ThenInclude(sccs => sccs!.CampusClass).ThenInclude(sccs => sccs!.Campus).Include(scss => scss.CampusClassSection!.Section).Include(scss => scss.CampusClassSection!.CampusClass!.Class).ToListAsync();

        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        //{
        //    var students = await _context.Students
        //        .Include(s => s.Gender)
        //        .Include(s => s.Shift)
        //        .Include(s => s.StudentCampusClassSection)
        //            .ThenInclude(sc => sc.CampusClassSection)
        //            .ThenInclude(ccs => ccs.CampusClass)
        //            .ThenInclude(cc => cc.Campus)
        //        .Include(s => s.StudentCampusClassSection)
        //            .ThenInclude(sc => sc.CampusClassSection)
        //            .ThenInclude(ccs => ccs.CampusClass)
        //            .ThenInclude(ccs => ccs.Class)
        //        .Include(s => s.StudentCampusClassSection)
        //            .ThenInclude(sc => sc.CampusClassSection)
        //            .ThenInclude(ccs => ccs.Section)
        //        .Select(s => new StudentDTO
        //        {
        //            StudentId = s.StudentId,
        //            StudentFName = s.StudentFName,
        //            StudentLName = s.StudentLName,
        //            FatherName = s.FatherName,
        //            MotherName = s.MotherName,
        //            DateOfBirth=s.DateOfBirth,
        //            BirthCertificateNumber = s.BirthCertificateNumber,
        //            Address = s.Address,
        //            GenderName = s.Gender.GenderName,
        //            ShiftName = s.Shift.ShiftName,
        //            CampusName = s.StudentCampusClassSection
        //                .Select(sc => sc.CampusClassSection.CampusClass.Campus.Name)
        //                .FirstOrDefault(),
        //            ClassName = s.StudentCampusClassSection
        //                .Select(sc => sc.CampusClassSection.CampusClass.Class.ClassName)
        //                .FirstOrDefault(),
        //            SectionName = s.StudentCampusClassSection
        //                .Select(sc => sc.CampusClassSection.Section.SectionName)
        //                .FirstOrDefault(),
        //            ImagePath = s.Image
        //        })
        //        .ToListAsync();

        //    return Ok(students);
        //}





        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }       

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(int id, [FromForm] StudentVM vm)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var shiftExists = await _context.Shifts.AnyAsync(s => s.ShiftId == vm.ShiftId);
            if (!shiftExists)
            {
                return BadRequest("Invalid ShiftId provided.");
            }

            student.StudentFName = vm.StudentFName;
            student.StudentLName = vm.StudentLName;
            student.FatherName = vm.FatherName;
            student.MotherName = vm.MotherName;
            student.DateOfBirth = vm.DateOfBirth;
            student.BirthCertificateNumber = vm.BirthCertificateNumber;
            student.Address = vm.Address;
            student.GenderId = vm.GenderId;
            student.ShiftId = vm.ShiftId;

            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                // Delete the existing image if necessary
                if (!string.IsNullOrEmpty(student.Image))
                {
                    var oldImagePath = Path.Combine(webroot, "Images", student.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImagePath.CopyToAsync(fileStream);
                }

                student.Image = fileName;
            }

            // Update or add related CampusClass and CampusClassSection entities if needed
            var campusClass = await _context.CampusClasses
                .FirstOrDefaultAsync(cc => cc.CampusId == vm.CampusId && cc.ClassId == vm.ClassId);

            if (campusClass == null)
            {
                campusClass = new CampusClass
                {
                    CampusId = vm.CampusId,
                    ClassId = vm.ClassId,
                };
                _context.CampusClasses.Add(campusClass);
            }

            var campusClassSection = await _context.CampusClassSections
                .FirstOrDefaultAsync(ccs => ccs.CampusClassId == campusClass.CampusClassId && ccs.SectionId == vm.SectionId);

            if (campusClassSection == null)
            {
                campusClassSection = new CampusClassSection
                {
                    CampusClass = campusClass,
                    SectionId = vm.SectionId,
                };
                _context.CampusClassSections.Add(campusClassSection);
            }

            var studentCampusClassSection = await _context.StudentCampusClassSections
                .FirstOrDefaultAsync(sccs => sccs.StudentId == student.StudentId);

            if (studentCampusClassSection == null)
            {
                studentCampusClassSection = new StudentCampusClassSection
                {
                    CampusClassSection = campusClassSection,
                    Student = student,
                };
                _context.StudentCampusClassSections.Add(studentCampusClassSection);
            }
            else
            {
                studentCampusClassSection.CampusClassSection = campusClassSection;
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromForm] StudentVM vm)
        {
            Student student = new Student
            {
                StudentFName = vm.StudentFName,
                StudentLName = vm.StudentLName,
                FatherName = vm.FatherName,
                MotherName = vm.MotherName,
                DateOfBirth = vm.DateOfBirth,
                BirthCertificateNumber = vm.BirthCertificateNumber,
                Address = vm.Address,
                GenderId = vm.GenderId,
                ShiftId = vm.ShiftId,
            };

            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                student.Image = fileName;
            }

            var campusClass = new CampusClass
            {
                CampusId = vm.CampusId,
                ClassId = vm.ClassId,
            };
            _context.Add(campusClass);

            var campusClassSection = new CampusClassSection
            {
                CampusClass = campusClass,
                CampusClassId = campusClass.CampusClassId,
                SectionId = vm.SectionId,
            };
            _context.Add(campusClassSection);

            var studentCampusClassSection = new StudentCampusClassSection
            {
                CampusClassSection = campusClassSection,
                StudentCampusClassSectionId = campusClassSection.CampusClassSectionId,
                Student = student,
                StudentId = student.StudentId
            };
            _context.Add(studentCampusClassSection);




            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            // Find the student and their related entities
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Find and remove StudentCampusClassSection
            var studentCampusClassSection = await _context.StudentCampusClassSections
                .FirstOrDefaultAsync(sccs => sccs.StudentId == student.StudentId);
            if (studentCampusClassSection != null)
            {
                _context.StudentCampusClassSections.Remove(studentCampusClassSection);
            }

            // Find and remove CampusClassSection if no other StudentCampusClassSection is linked to it
            var campusClassSection = studentCampusClassSection?.CampusClassSection;
            if (campusClassSection != null)
            {
                var otherLinks = await _context.StudentCampusClassSections
                    .AnyAsync(sccs => sccs.CampusClassSectionId == campusClassSection.CampusClassSectionId && sccs.StudentId != student.StudentId);
                if (!otherLinks)
                {
                    _context.CampusClassSections.Remove(campusClassSection);
                }
            }

            // Find and remove CampusClass if no other CampusClassSection is linked to it
            var campusClass = campusClassSection?.CampusClass;
            if (campusClass != null)
            {
                var otherSections = await _context.CampusClassSections
                    .AnyAsync(ccs => ccs.CampusClassId == campusClass.CampusClassId && ccs.CampusClassSectionId != campusClassSection.CampusClassSectionId);
                if (!otherSections)
                {
                    _context.CampusClasses.Remove(campusClass);
                }
            }

            // Finally, remove the student and save changes
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}