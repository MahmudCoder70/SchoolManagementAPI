using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class StudentVM
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; } = default!;
        public string StudentLName { get; set; } = default!;
        public string FatherName { get; set; } = default!;
        public string MotherName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;
        public string? Image { get; set; }
        public IFormFile? ImagePath { get; set; }
        public string BirthCertificateNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public int CampusId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
    }
}

