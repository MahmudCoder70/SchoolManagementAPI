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
    public class CampusController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CampusController(SchoolDbContext context)
        {
            _context = context;
        }

        

        // GET: api/Campus
        [Route("GetCampus")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campus>>> GetCampuss()
        {
            return await _context.Campus.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CampusVM>> GetCampus(int id)
        {
            Campus campus = await _context.Campus.FindAsync(id);
            Class[] classList = _context.CampusClasses.Where(x => x.CampusId == campus.CampusId).Select(x => new Class { ClassId = x.ClassId }).ToArray();

            CampusVM campusVM = new CampusVM()
            {
                CampusId = campus.CampusId,
                Name = campus.Name,
                CurriculumId = campus.CurriculumId,
                SchoolId = campus.SchoolId,
               
                GenderId = campus.GenderId,
                classList = classList
            };
            return campusVM;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampusVM>>> GetCampusClass()
        {
            List<CampusVM> CampusClassList = new List<CampusVM>();
            var allCampus = _context.Campus.ToList();
            foreach (var campus in allCampus)
            {
                var classList = _context.CampusClasses.Where(x => x.CampusId == campus.CampusId).Select(x => new Class
                {
                    ClassId = x.ClassId,
                    ClassName = x.Class.ClassName
                }).ToArray();

                CampusClassList.Add(new CampusVM
                {
                    CampusId = campus.CampusId,
                    Name = campus.Name,
                    CurriculumId = campus.CurriculumId,
                    SchoolId = campus.SchoolId,
                    
                    GenderId = campus.GenderId,
                    classList = classList

                });
            }
            return CampusClassList;
        }
        

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<CampusClass>> UpdateCampusClass([FromForm] CampusVM vm)
        {
            var ClassItem = JsonConvert.DeserializeObject<Class[]>(vm.ClassStringify);

            Campus campus = _context.Campus.Find(vm.CampusId);
            campus.CampusId = vm.CampusId;
            campus.Name = vm.Name;
            campus.CurriculumId = vm.CurriculumId;
            campus.SchoolId = campus.SchoolId;
           ;
            campus.GenderId = campus.GenderId;


            //Delete existing class
            var existingClass = _context.CampusClasses.Where(x => x.CampusId == campus.CampusId).ToList();
            foreach (var item in existingClass)
            {
                _context.CampusClasses.Remove(item);
            }

            //Add newly added class
            foreach (var item in ClassItem)
            {
                var campusClass = new CampusClass
                {
                    CampusId = campus.CampusId,
                    ClassId = item.ClassId

                };
                _context.Add(campusClass);
            }

            _context.Entry(campus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(campus);
        }

        [HttpPost]
        public async Task<ActionResult<CampusClass>> PostCampusClass([FromForm] CampusVM vm)
        {
            var classItems = JsonConvert.DeserializeObject<Class[]>(vm.ClassStringify);

            Campus campus = new Campus
            {
                Name = vm.Name,
                CurriculumId = vm.CurriculumId,
             
                SchoolId = vm.SchoolId,
                GenderId = vm.GenderId
            };

            //for SkillList

            foreach (var item in classItems)
            {
                var campusClass = new CampusClass
                {
                    Campus = campus,
                    CampusId = campus.CampusId,
                    ClassId = item.ClassId
                };
                _context.Add(campusClass);
            }
            await _context.SaveChangesAsync();
            return Ok(campus);
        }
        // DELETE: api/Campus/5
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<CampusClass>> DeleteCampusClass(int id)
        {
            Campus campus = _context.Campus.Find(id);

            var existingclass = _context.CampusClasses.Where(x => x.CampusId == campus.CampusId).ToList();
            foreach (var item in existingclass)
            {
                _context.CampusClasses.Remove(item);
            }
            _context.Entry(campus).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return Ok(campus);
        }

        private bool CampusExists(int id)
        {
            return _context.Campus.Any(e => e.CampusId == id);
        }
    }
}
