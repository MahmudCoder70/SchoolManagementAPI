using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class RoutineConfigurationVM
    {
        public int RoutineConfigurationId { get; set; }

        public string ConfigName { get; set; }

        public string ConfigValue { get; set; }

        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }

        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        public virtual Campus? Campus { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}
