using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Models.ViewModel;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public RoutinesController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetRoutines()
        {
            var routines = await _context.Routines
                .Include(r => r.Campus)
                .Include(r => r.Section)
                .Include(r => r.Class)
                .Include(r => r.Subject)
                .Include(r => r.Teacher)
                .Include(r => r.Shift)
                .Select(r => new
                {
                    r.RoutineId,
                    CampusName = r.Campus.Name,
                    SectionName = r.Section.SectionName,
                    ClassName = r.Class.ClassName,
                    SubjectName = r.Subject.SubjectName,
                    TeacherName = r.Teacher.TeacherName,
                    ShiftName = r.Shift.ShiftName,
                    r.ClassStartTime,
                    r.ClassDuration,
                    r.Weekdays,
                    r.Year,
                    r.IsFirstClass,
                    r.IsAfterTiffinClass
                })
                .ToListAsync();

            return Ok(routines);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoutine([FromForm] RoutineVM vm)
        {
            if (vm == null)
            {
                return BadRequest("Routine cannot be null.");
            }
            Routine routine = new Routine()
            {
                CampusId = vm.CampusId,
                SectionId = vm.SectionId,
                ClassId = vm.ClassId,
                SubjectId = vm.SubjectId,
                TeacherId = vm.TeacherId,
                ShiftId = vm.ShiftId,
                Weekdays = vm.Weekdays,
                Year = vm.Year,
            };

            // Fetch the configuration values from the database
            var configuration = await _context.RoutineConfigurations
                .Where(c => c.CampusId == vm!.CampusId && c.ShiftId == vm.ShiftId)
                .ToListAsync();

            // Retrieve the specific configuration values
            var firstClassStartTime = configuration.FirstOrDefault(c => c.ConfigName == "First Class Start Time")?.ConfigValue;
            var firstClassDuration = configuration.FirstOrDefault(c => c.ConfigName == "First Class Duration")?.ConfigValue;
            var otherClassDuration = configuration.FirstOrDefault(c => c.ConfigName == "Other Class Duration")?.ConfigValue;

            // Handle the condition when it's the first class
            if (vm.IsFirstClass == true)
            {

                routine.ClassStartTime = decimal.Parse(firstClassStartTime.ToString()); // Convert to appropriate format if necessary
                routine.ClassDuration = int.Parse(firstClassDuration.ToString());


            }
            else
            {
                routine.ClassStartTime = decimal.Parse(firstClassStartTime.ToString()) + decimal.Parse(firstClassDuration) + 5;
                routine.ClassDuration = int.Parse(otherClassDuration.ToString());

            }

            // Save the routine entity in the database
            _context.Routines.Add(routine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoutinebyId), new { id = routine.RoutineId }, routine);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoutineVM>> GetRoutinebyId(int id)
        {
            // Load the routine with related entities using eager loading
            var routine = await _context.Routines
                .Include(r => r.Campus)
                .Include(r => r.Section)
                .Include(r => r.Class)
                .Include(r => r.Subject)
                .Include(r => r.Teacher)
                .Include(r => r.Shift)
                .FirstOrDefaultAsync(r => r.RoutineId == id);

            if (routine == null)
            {
                return NotFound($"Routine with ID {id} not found.");
            }

            // Map the routine and related entity names to the RoutineVM
            var routineVM = new RoutineVM()
            {
                RoutineId = routine.RoutineId,
                CampusId = routine.CampusId,     
                SectionId = routine.SectionId,   
                ClassId = routine.ClassId,         
                SubjectId = routine.SubjectId,
                TeacherId = routine.TeacherId,
                ShiftId = routine.ShiftId,
                ClassStartTime = routine.ClassStartTime,
                ClassDuration = routine.ClassDuration,
                Weekdays = routine.Weekdays,
                Year = routine.Year,
                IsFirstClass = routine.IsFirstClass,
                IsAfterTiffinClass = routine.IsAfterTiffinClass
            };

            return Ok(routineVM);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutine(int id)
        {
            // Find the routine in the database
            var routine = await _context.Routines.FindAsync(id);
            if (routine == null)
            {
                return NotFound($"Routine with ID {id} not found.");
            }

            // Remove the routine from the database
            _context.Routines.Remove(routine);
            await _context.SaveChangesAsync();

            return NoContent(); // Return a success response with no content
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoutine(int id, [FromForm] RoutineVM vm)
        {
            if (vm == null)
            {
                return BadRequest("Routine cannot be null.");
            }

            var existingRoutine = await _context.Routines.FindAsync(id);
            if (existingRoutine == null)
            {
                return NotFound($"Routine with ID {id} not found.");
            }

            // Update routine properties
            existingRoutine.CampusId = vm.CampusId;
            existingRoutine.SectionId = vm.SectionId;
            existingRoutine.ClassId = vm.ClassId;
            existingRoutine.SubjectId = vm.SubjectId;
            existingRoutine.TeacherId = vm.TeacherId;
            existingRoutine.ShiftId = vm.ShiftId;
            existingRoutine.Weekdays = vm.Weekdays;
            existingRoutine.Year = vm.Year;


            // Fetch the configuration values from the database
            var configuration = await _context.RoutineConfigurations
                .Where(c => c.CampusId == vm!.CampusId && c.ShiftId == vm.ShiftId)
                .ToListAsync();

            // Retrieve the specific configuration values
            var firstClassStartTime = configuration.FirstOrDefault(c => c.ConfigName == "First Class Start Time")?.ConfigValue;
            var firstClassDuration = configuration.FirstOrDefault(c => c.ConfigName == "First Class Duration")?.ConfigValue;
            var otherClassDuration = configuration.FirstOrDefault(c => c.ConfigName == "Other Class Duration")?.ConfigValue;

            // Handle the condition when it's the first class
            if (vm.IsFirstClass == true)
            {
                existingRoutine.ClassStartTime = decimal.Parse(firstClassStartTime.ToString());
                existingRoutine.ClassDuration = int.Parse(firstClassDuration.ToString());
            }
            else
            {
                existingRoutine.ClassStartTime = decimal.Parse(firstClassStartTime.ToString()) + decimal.Parse(firstClassDuration) + 5;
                existingRoutine.ClassDuration = int.Parse(otherClassDuration.ToString());
            }

            // Save changes to the database
            _context.Routines.Update(existingRoutine);
            await _context.SaveChangesAsync();

            return NoContent(); // Return a success response with no content
        }

    }
}