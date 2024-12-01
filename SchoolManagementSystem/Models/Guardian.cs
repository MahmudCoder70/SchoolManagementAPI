using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Guardian
    {
        public int GuardianId { get; set; }
        public string GuardianName { get; set; } = default!;
        public string Phone { get; set; } =default!;
        public string NIDNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        
        public virtual List<StudentGuardian> StudentGuardians { get; set; } = new List<StudentGuardian>();


    }
}
