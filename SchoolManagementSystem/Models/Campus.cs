using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        public string Name { get; set; } = default!;
        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
       
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public virtual Curriculum? Curriculum { get; set; }
        public virtual School? School { get; set; }
       
        public virtual Gender? Gender { get; set; }
        public virtual ICollection<CampusClass> SchoolCampuses { get; set; } = new List<CampusClass>();
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual List<RoutineConfiguration> RoutineConfigurations { get; set; } = new List<RoutineConfiguration>();


    }
}
