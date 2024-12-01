namespace SchoolManagementSystem.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = default!;
        public  decimal StartTime { get; set; }
        public decimal EndTime { get; set; }
        public virtual List<RoutineConfiguration> RoutineConfigurations { get; set; } = new List<RoutineConfiguration>();
        public virtual List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual List<Student> Students { get; set; } = new List<Student>();

    }
}
