using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Routine
    {
        public int RoutineId { get; set; }
        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Section")]
        public int SectionId {  get; set; }
        [ForeignKey("Class")]
        public int ClassId {  get; set; }
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
        public bool  IsFirstClass { get; set; }
        public bool IsAfterTiffinClass { get; set; }

        //nev
        public virtual Campus? Campus { get; set; }
        public virtual Section? Section { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}
