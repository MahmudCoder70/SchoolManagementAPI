using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = default!;
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        public virtual Class? Class { get; set; }
       
        public virtual Curriculum? Curriculum { get; set; }  
        public virtual List<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();

    }

}
