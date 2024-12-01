using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class RoutineConfiguration
    {
        public int RoutineConfigurationId { get; set; }

        public string ConfigName { get; set; }

        public string ConfigValue { get; set; }

        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }

        public virtual Curriculum? Curriculum { get; set; }
        public virtual Campus? Campus { get; set; }
        public virtual Shift? Shift { get; set; }
    }
}

//public int DailyClassNo { get; set; }
//public string FirstClassDuration { get; set; }
//public string AfterTiffenClassDuration { get; set; }
//public string OtherClassDuration { get; set; }
//public string TiffinBreak { get; set; }
//public string BreakBetweenClasses { get; set; }
//public string MaxNumberOfClassTeacher { get; set; }
//public string FirstClassNoOfATeacher { get; set; }
//public int MaxNoOfClassForATeacher { get; set; }
//public int MinNoOfClassForATeacher { get; set; }
//public int WeaklyTeachersClassNo { get; set; }