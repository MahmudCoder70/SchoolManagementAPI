using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class ClassTeacher
    {
        public int ClassTeacherId { get; set; }
        [ForeignKey("CampusClassSection")]
        public int CampusClassSectionId { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [ForeignKey("Routine")]
        public int RoutineId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Subject? Subject { get; set; }    
        public virtual Routine? Routine { get; set; }
        public virtual CampusClassSection? CampusClassSection { get; set; }
    }
}
