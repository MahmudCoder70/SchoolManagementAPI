using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models.ViewModel;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineConfigurationsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public RoutineConfigurationsController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutineConfigurations()
        {
            var routineConfigurations = await _context.RoutineConfigurations
                .Include(rc => rc.Campus)
                .Include(rc => rc.Shift)
                .Include(rc=>rc.Curriculum)
                .ToListAsync();

            return Ok(routineConfigurations);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoutineConfigurationVM>> GetConfiguration(int id)
        {
            RoutineConfiguration routineConfiguration = await _context.RoutineConfigurations.FindAsync(id);


            RoutineConfigurationVM routineConfigurationVM = new RoutineConfigurationVM()
            {
                RoutineConfigurationId = routineConfiguration.RoutineConfigurationId,
                ConfigName = routineConfiguration.ConfigName,
                ConfigValue = routineConfiguration.ConfigValue,
                CampusId = routineConfiguration.CampusId,
                ShiftId = routineConfiguration.ShiftId,
                CurriculumId= routineConfiguration.CurriculumId,

            };
            return routineConfigurationVM;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<RoutineConfiguration>> UpdateConfiguration([FromBody] RoutineConfigurationVM vm, int id)
        {
            var routineConfiguration = await _context.RoutineConfigurations.FindAsync(id);
            //var student = await _context.Students.FindAsync(id);
            routineConfiguration.ConfigName = vm.ConfigName;
            routineConfiguration.ConfigValue = vm.ConfigValue;
            routineConfiguration.CampusId = vm.CampusId;
            routineConfiguration.ShiftId = vm.ShiftId;
            routineConfiguration.CurriculumId= vm.CurriculumId;


            _context.Entry(routineConfiguration).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(routineConfiguration);
        }
        [HttpPost]
        public async Task<ActionResult<RoutineConfiguration>> PostConfiguration([FromBody]RoutineConfigurationVM vm)
        {
            RoutineConfiguration routineConfiguration = new RoutineConfiguration()

            {
                ConfigName = vm.ConfigName,
                ConfigValue=vm.ConfigValue,
                CampusId = vm.CampusId,
                ShiftId = vm.ShiftId,
                CurriculumId= vm.CurriculumId,

            };
            _context.RoutineConfigurations.Add(routineConfiguration);
            await _context.SaveChangesAsync();
            return Ok(routineConfiguration);
        }
        private bool ConfigurationExists(int id)
        {
            return _context.RoutineConfigurations.Any(e => e.RoutineConfigurationId == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RoutineConfiguration>> DeleteConfiguration(int id)
        {
            RoutineConfiguration routineConfiguration = _context.RoutineConfigurations.Find(id);

            var existingConfiguration = _context.RoutineConfigurations.Where(x => x.RoutineConfigurationId == routineConfiguration.RoutineConfigurationId).ToList();
            foreach (var item in existingConfiguration)
            {
                _context.RoutineConfigurations.Remove(item);
            }
            _context.Entry(routineConfiguration).State = EntityState.Deleted;
            _context.RoutineConfigurations.Remove(routineConfiguration);
            await _context.SaveChangesAsync();

            return Ok(routineConfiguration);
        }
    }
}
