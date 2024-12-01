namespace SchoolManagementSystem.Models.ViewModel
{
    public class ShiftVM
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; } = default!;
        public decimal StartTime { get; set; }
        public decimal EndTime { get; set; }

    }
}