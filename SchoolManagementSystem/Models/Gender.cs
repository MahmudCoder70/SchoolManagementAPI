namespace SchoolManagementSystem.Models
{
    public class Gender
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; } = default!;
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual ICollection<Campus> Campuses { get; set; } = new List<Campus>();
    }
}
