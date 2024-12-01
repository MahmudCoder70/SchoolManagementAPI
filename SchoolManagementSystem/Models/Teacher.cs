 using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? TeacherImage { get; set; }
        public string Qualification { get; set; } = default!;
        public DateOnly JoinDate { get; set; } = default!;
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }    
        public string AcademicYear { get; set; } = default!;
        public virtual Gender? Gender { get; set; }
        public virtual Campus? Campus { get; set; }
        public virtual Section? Section { get; set; }
        public virtual Shift? Shift { get; set; }
        public virtual List<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
        public virtual List<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();
  


    }
}
