namespace SchoolManagementSystem.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = default!;
        public virtual List<CampusClass> CampusClass { get; set; } = new List<CampusClass>();
        public virtual List<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
