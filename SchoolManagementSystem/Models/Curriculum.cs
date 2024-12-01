namespace SchoolManagementSystem.Models
{
    public class Curriculum
    {
        public int CurriculumId { get; set; }
        public string CurriculumName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public virtual List<Campus> Campuses { get; set; } = new List<Campus>();
        public virtual List<Subject> Subjects { get; set; } = new List<Subject>();
        public virtual List<RoutineConfiguration> RoutineConfigurations { get; set; } = new List<RoutineConfiguration>();

    }
}
