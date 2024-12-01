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
    public class GuardiansController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public GuardiansController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/Guardians
        [HttpGet]
        [Route("GetGuardian")]
        public async Task<ActionResult<IEnumerable<Guardian>>> GetGuardians()
        {
            return await _context.Guardians.ToListAsync();
        }
        // GET: api/Student
        [HttpGet]
        [Route("GetStudent")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Guardians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuardianVM>> GetGuardian(int id)
        {
            Guardian guardian = await _context.Guardians.FindAsync(id);
            Student[] studList = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).Select(x => new Student { StudentId = x.StudentId }).ToArray();

            GuardianVM guardianVM = new GuardianVM()
            {
                GuardianId = guardian.GuardianId,
                GuardianName = guardian.GuardianName,              
                Phone = guardian.Phone,
                NIDNumber = guardian.NIDNumber,
                Email = guardian.Email,
                
                studentsList = studList
            };
            return guardianVM;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuardianVM>>> GetStudentGuardian()
        {
            List<GuardianVM> studentGuardianList = new List<GuardianVM>();
            var allGuardians = _context.Guardians.ToList();
            foreach (var guardian in allGuardians)
            {
                var studentList = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).Select(x => new Student
                {
                    StudentId = x.StudentId,
                    StudentFName = x.Student.StudentFName,
                    StudentLName = x.Student.StudentLName,
                    FatherName = x.Student.FatherName,
                    MotherName = x.Student.MotherName,
                    DateOfBirth = x.Student.DateOfBirth,
                    BirthCertificateNumber = x.Student.BirthCertificateNumber,
                    Address = x.Student.Address,
                    GenderId = x.Student.GenderId
                }).ToArray();

                studentGuardianList.Add(new GuardianVM
                {
                    GuardianId = guardian.GuardianId,
                    GuardianName = guardian.GuardianName,
                    Phone = guardian.Phone,
                    NIDNumber = guardian.NIDNumber,
                    Email = guardian.Email,
                    studentsList = studentList

                });
            }
            return studentGuardianList;
        }


        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<StudentGuardian>> UpdateStudentGuardian([FromForm] GuardianVM vm)
        {
            var studentList = JsonConvert.DeserializeObject<Student[]>(vm.studentStringify);

            Guardian guardian = _context.Guardians.Find(vm.GuardianId);
            guardian.GuardianId = vm.GuardianId;
            guardian.GuardianName=vm.GuardianName;
            guardian.Phone=vm.Phone;
            guardian.NIDNumber=vm.NIDNumber;
            guardian.Email=vm.Email;

            //Delete existing spots
            var existingStudent = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).ToList();
            foreach (var item in existingStudent)
            {
                _context.StudentGuardian.Remove(item);
            }

            //Add newly added spots
            foreach (var item in studentList)
            {
                var studentGuardian = new StudentGuardian
                {
                    GuardianId = guardian.GuardianId,
                    StudentId = item.StudentId

                };
                _context.Add(studentGuardian);
            }

            _context.Entry(guardian).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(guardian);
        }


        [HttpPost]
        public async Task<ActionResult<StudentGuardian>> PostStudentGuardian([FromForm] GuardianVM vm)
        {
            var studentList = JsonConvert.DeserializeObject<Student[]>(vm.studentStringify);

            Guardian guardian = new Guardian
            {
                GuardianName = vm.GuardianName,
                Phone = vm.Phone,
                NIDNumber = vm.NIDNumber,
                Email = vm.Email
            };


            foreach (var item in studentList)
            {
                var studentGuardian = new StudentGuardian
                {
                    Guardian = guardian,
                    GuardianId = guardian.GuardianId,
                    StudentId = item.StudentId
                };
                _context.Add(studentGuardian);
            }
            await _context.SaveChangesAsync();
            return Ok(guardian);
        }

        private bool GuardianExists(int id)
        {
            return _context.Guardians.Any(e => e.GuardianId == id);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<StudentGuardian>> DeleteStudentGuardian(int id)
        {
            Guardian guardian = _context.Guardians.Find(id);

            var existingStudent = _context.StudentGuardian.Where(x => x.GuardianId == guardian.GuardianId).ToList();
            foreach (var item in existingStudent)
            {
                _context.StudentGuardian.Remove(item);
            }
            _context.Entry(guardian).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(guardian);
        }

    }
}
