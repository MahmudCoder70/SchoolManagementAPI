using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class TeacherSubject
    {
        public int TeacherSubjectId { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
