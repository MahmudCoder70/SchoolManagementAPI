using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class TeacherVM
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? TeacherImage { get; set; }
        [Display(Name ="Image")]
        public IFormFile? ImagePath { get; set; }
        public string Qualification { get; set; } = default!;
        public DateOnly JoinDate { get; set; } = default!;
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public string AcademicYear { get; set; }= default!;
        //public virtual Gender? Gender { get; set; }
        //public virtual Campus? Campus { get; set; }
        //public virtual Section? Section { get; set; }
        //public virtual Shift? Shift { get; set; }
        //public virtual AcademicYear? AcademicYear { get; set; }
        public string? subjectStringify { get; set; }
        public string? classStringify { get; set; }


        public Subject[]? subjectsList { get; set; }
        public Class[]? classList { get; set; }

    }
}
