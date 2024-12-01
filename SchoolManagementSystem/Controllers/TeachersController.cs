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
    public class TeachersController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        private readonly IWebHostEnvironment _env;

        public TeachersController(SchoolDbContext _context, IWebHostEnvironment _env)
        {
            this._context = _context;
            this._env = _env;
        }

        [HttpGet]
        [Route("GetSubject")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubject()
        {
            return await _context.Subjects.ToListAsync();
        }

        // GET: api/Teachers
        [HttpGet]
        [Route("GetTeacher")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.Include(x=>x.Campus.School).ToListAsync();
            return await _context.Teachers.Include(x=>x.Campus.School).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherVM>> GetTeacher(int id)
        {
            Teacher teacher = await _context.Teachers.FindAsync(id);
            Subject[] subList = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).Select(x => new Subject { SubjectId = x.SubjectId }).ToArray();
            Class[] classList = _context.TeacherClass.Where(x => x.TeacherId == teacher.TeacherId).Select(x => new Class { ClassId = x.ClassId }).ToArray();

            TeacherVM teacherVM = new TeacherVM()
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                DateOfBirth = teacher.DateOfBirth,
                Phone = teacher.Phone,
                Qualification = teacher.Qualification,
                TeacherImage = teacher.TeacherImage,
                JoinDate = teacher.JoinDate,
                GenderId = teacher.GenderId,
                SectionId = teacher.SectionId,
                ShiftId = teacher.ShiftId,
                CampusId = teacher.CampusId,
                AcademicYear = teacher.AcademicYear,
                subjectsList= subList,
                classList= classList,
            };
            return teacherVM;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherVM>>> GetTeacherSubject()
        {
            List<TeacherVM> TeacherList = new List<TeacherVM>();
            

            var allTeacher = _context.Teachers.ToList();
            foreach (var teacher in allTeacher)
            {
                var subjectList = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).Select(x => new Subject
                {
                    SubjectId = x.SubjectId,
                    SubjectName = x.Subject.SubjectName,
                    ClassId=x.Subject.ClassId,
                    CurriculumId=x.Subject.CurriculumId
                }).ToArray();
                var classList = _context.TeacherClass.Where(x => x.TeacherId == teacher.TeacherId).Select(x => new Class
                {
                    ClassId = x.ClassId,
                    ClassName = x.Class.ClassName,
                }).ToArray();

                TeacherList.Add(new TeacherVM
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    DateOfBirth = teacher.DateOfBirth,
                    Phone = teacher.Phone,
                    TeacherImage = teacher.TeacherImage,
                    Qualification = teacher.Qualification,
                    JoinDate = teacher.JoinDate,
                    GenderId = teacher.GenderId,
                    SectionId = teacher.SectionId,
                    CampusId = teacher.CampusId,
                    ShiftId = teacher.ShiftId,
                    AcademicYear = teacher.AcademicYear,
                    subjectsList = subjectList,
                    classList = classList,

                });
            }
            return TeacherList;
        }

       
        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Teacher>> UpdateTeacherSubject([FromForm] TeacherVM vm)
        {
            var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);
            var classItems = JsonConvert.DeserializeObject<Class[]>(vm.classStringify);


            Teacher teacher = _context.Teachers.Find(vm.TeacherId);
            teacher.TeacherName = vm.TeacherName;
            teacher.DateOfBirth = vm.DateOfBirth;
            teacher.TeacherImage= vm.TeacherImage;
            teacher.Phone = vm.Phone;
            teacher.Qualification = vm.Qualification;
            teacher.JoinDate = vm.JoinDate;
            teacher.SectionId = vm.SectionId;
            teacher.CampusId = vm.CampusId;
            teacher.GenderId = vm.GenderId;
            teacher.ShiftId = vm.ShiftId;
            teacher.AcademicYear = vm.AcademicYear;



            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                teacher.TeacherImage = fileName;
            }

            //Delete existing subjects
            var existingSubject = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var item in existingSubject)
            {
                _context.TeachersSubject.Remove(item);
            }
            //Delete existing Class
            var existingClass = _context.TeacherClass.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var items in existingClass)
            {
                _context.TeacherClass.Remove(items);
            }

            //Add newly added Subjects
            foreach (var item in subjectItems)
            {
                var teacherSubject = new TeacherSubject
                {
                    TeacherId = teacher.TeacherId,
                    SubjectId = item.SubjectId

                };
                _context.Add(teacherSubject);
            }
            //Add newly added Class
            foreach (var item in classItems)
            {
                var teacherClass = new TeacherClass
                {
                    TeacherId = teacher.TeacherId,
                    ClassId = item.ClassId,

                };
                _context.Add(teacherClass);
            }

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher([FromForm] TeacherVM vm)
        {
            var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);
            var classItems = JsonConvert.DeserializeObject<Class[]>(vm.classStringify);


            Teacher teacher = new Teacher
            {
                TeacherName = vm.TeacherName,
                DateOfBirth = vm.DateOfBirth,
                Phone = vm.Phone,
                Qualification = vm.Qualification,
                JoinDate = vm.JoinDate,
                SectionId = vm.SectionId,
                CampusId = vm.CampusId,
                GenderId = vm.GenderId,
                ShiftId = vm.ShiftId,
                AcademicYear = vm.AcademicYear,
              
            };
            //For Image
            if (vm.ImagePath != null)
            {
                var webroot = _env.WebRootPath;
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(vm.ImagePath.FileName);
                var filePath = Path.Combine(webroot, "Images", fileName);

                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await vm.ImagePath.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                teacher.TeacherImage = fileName;
            }
            //for SubjectList

            foreach (var item in subjectItems)
            {
                var teacherSubject = new TeacherSubject
                {
                    Teacher = teacher,
                    TeacherId = teacher.TeacherId,
                    SubjectId = item.SubjectId
                };
                _context.Add(teacherSubject);
            }
            //for ClassList

            foreach (var item in classItems)
            {
                var teacherclass = new TeacherClass
                {
                    Teacher = teacher,
                    TeacherId = teacher.TeacherId,
                    ClassId = item.ClassId
                };
                _context.Add(teacherclass);
            }

            await _context.SaveChangesAsync();
            return Ok(teacher);
        }


        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<Teacher>> DeleteTeacherSubject(int id)
        {
            Teacher teacher = _context.Teachers.Find(id);

            var existingSubject = _context.TeachersSubject.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var item in existingSubject)
            {
                _context.TeachersSubject.Remove(item);
            }
            var existingClass = _context.TeacherClass.Where(x => x.TeacherId == teacher.TeacherId).ToList();
            foreach (var items in existingClass)
            {
                _context.TeacherClass.Remove(items);
            }
            _context.Entry(teacher).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(teacher);
        }
    }
}
