using System;
using System.Collections.Generic;
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
    public class BuildingsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public BuildingsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/Buildings

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingVM>> GetBuilding(int id)
        {
            Building building = await _context.Buildings.FindAsync(id);


            BuildingVM buildingVM = new BuildingVM()
            {
                BuildingId = building.BuildingId,
                BuildingName = building.BuildingName,

                SchoolId = building.SchoolId,

            };
            return buildingVM;
        }


        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<Building>> UpdateBuilding([FromForm] BuildingVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Building building = _context.Buildings.Find(vm.BuildingId);
            building.BuildingName = vm.BuildingName;
            building.SchoolId = vm.SchoolId;


            _context.Entry(building).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(building);
        }
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding([FromForm] BuildingVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            Building building = new Building()
            {
                BuildingName = vm.BuildingName,
                SchoolId = vm.SchoolId,


            };
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();
            return Ok(building);
        }


        private bool BuildingExists(int id)
        {
            return _context.Buildings.Any(e => e.BuildingId == id);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<Building>> DeleteBuilding(int id)
        {
            Building building = _context.Buildings.Find(id);

            var existingBuilding = _context.Buildings.Where(x => x.BuildingId == building.BuildingId).ToList();
            foreach (var item in existingBuilding)
            {
                _context.Buildings.Remove(item);
            }
            _context.Entry(building).State = EntityState.Deleted;
            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return Ok(building);
        }
    }
}

