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
    public class BuildingRoomsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public BuildingRoomsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/BuildingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingRoom>>> GetBuildingRooms()
        {
            return await _context.BuildingRooms.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingRoomVM>> GetBuildingRoom(int id)
        {
            BuildingRoom buildingRoom = await _context.BuildingRooms.FindAsync(id);


            BuildingRoomVM buildingRoomVM = new BuildingRoomVM()
            {
                BuildingRoomId = buildingRoom.BuildingRoomId,
                RoomNumber = buildingRoom.RoomNumber,
                BuildingId = buildingRoom.BuildingId,

            };
            return buildingRoomVM;
        }

        [HttpPost]
        public async Task<ActionResult<BuildingRoom>> PostBuildingRoom([FromForm] BuildingRoomVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            BuildingRoom buildingRoom = new BuildingRoom()
            {
                RoomNumber = vm.RoomNumber,
                BuildingId = vm.BuildingId,


            };
            _context.BuildingRooms.Add(buildingRoom);
            await _context.SaveChangesAsync();
            return Ok(buildingRoom);
        }

        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<BuildingRoom>> UpdateBuildingRoom([FromForm] BuildingRoomVM vm)
        {
            //var subjectItems = JsonConvert.DeserializeObject<Subject[]>(vm.subjectStringify);

            BuildingRoom buildingRoom = _context.BuildingRooms.Find(vm.BuildingRoomId);
            buildingRoom.RoomNumber = vm.RoomNumber;
            buildingRoom.BuildingId = vm.BuildingId;


            _context.Entry(buildingRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(buildingRoom);
        }

        private bool BuildingRoomExists(int id)
        {
            return _context.BuildingRooms.Any(e => e.BuildingRoomId == id);
        }
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<ActionResult<BuildingRoom>> DeleteBuildingRoom(int id)
        {
            BuildingRoom buildingRoom = _context.BuildingRooms.Find(id);

            var existingBuildingRoom = _context.BuildingRooms.Where(x => x.BuildingRoomId == buildingRoom.BuildingRoomId).ToList();
            foreach (var item in existingBuildingRoom)
            {
                _context.BuildingRooms.Remove(item);
            }
            _context.Entry(buildingRoom).State = EntityState.Deleted;
            _context.BuildingRooms.Remove(buildingRoom);
            await _context.SaveChangesAsync();

            return Ok(buildingRoom);
        }
    }
}
