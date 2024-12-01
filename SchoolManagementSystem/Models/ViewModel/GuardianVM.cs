namespace SchoolManagementSystem.Models.ViewModel
{
    public class GuardianVM
    {
        public int GuardianId { get; set; }
        public string GuardianName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string NIDNumber { get; set; } = default!;
        public string Email { get; set; } = default!;

        public string? studentStringify { get; set; }

        public Student[]? studentsList { get; set; }
    }
}
