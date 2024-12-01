using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class StudentGuardian
    {
        public int StudentGuardianId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Guardian")]
        public int GuardianId { get; set; }
        public virtual Student? Student { get; set;}
        public virtual Guardian? Guardian { get; set; }
    }
}
