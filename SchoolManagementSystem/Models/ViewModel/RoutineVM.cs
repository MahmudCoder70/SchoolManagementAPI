using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class RoutineVM
    {
        public int RoutineId { get; set; }
        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        [ForeignKey("Class")]
        public int ClassId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public decimal ClassStartTime { get; set; }
        public int ClassDuration { get; set; }
        public string Weekdays { get; set; } = default!;
        public int Year { get; set; }

        public bool IsFirstClass { get; set; }
        public bool IsAfterTiffinClass { get; set; }




    }
}