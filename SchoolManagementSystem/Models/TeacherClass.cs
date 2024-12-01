using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class TeacherClass
    {
        public int TeacherClassId { get; set; }
        [ForeignKey("Teacher")]

        public int TeacherId { get; set; }
        [ForeignKey("Class")]

        public int ClassId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Class? Class { get; set; }

    }
}
