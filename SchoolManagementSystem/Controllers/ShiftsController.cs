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
    public class ShiftsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public ShiftsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/Shifts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Shift>> GetShift(int id)
        //{
        //    var shift = await _context.Shifts.FindAsync(id);

        //    if (shift == null)
        //    {
        //        return NotFound();
        //    }

        //    return shift;
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftVM>> GetShift(int id)
        {
            Shift shift = await _context.Shifts.FindAsync(id);


            ShiftVM shiftVM = new ShiftVM()
            {
                ShiftId = shift.ShiftId,
                ShiftName = shift.ShiftName,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime
            };
            return shiftVM;
        }



        [Route("Update")]
        [HttpPut]
        public async Task<ActionResult<ShiftVM>> UpdateShift([FromForm] ShiftVM vm)
        {

            Shift shift = _context.Shifts.Find(vm.ShiftId);
            shift.ShiftName = vm.ShiftName;
            shift.StartTime = vm.StartTime;
            shift.EndTime = vm.EndTime;

            _context.Entry(shift).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(shift);
        }

        // POST: api/Shifts
       
        [HttpPost]
        public async Task<ActionResult<Shift>> PostShift([FromForm] Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShift", new { id = shift.ShiftId }, shift);
        }

        // DELETE: api/Shifts/5
        [Route("Delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            _context.Entry(shift).State = EntityState.Deleted;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(int id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }
    }
}