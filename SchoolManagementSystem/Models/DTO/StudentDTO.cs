namespace SchoolManagementSystem.Models.DTO
{
    public class StudentDTO
    {
            public int StudentId { get; set; }
            public string StudentFName { get; set; }
            public string StudentLName { get; set; }
            public string FatherName { get; set; }
            public string MotherName { get; set; }
            public DateOnly DateOfBirth { get; set; }
            public string BirthCertificateNumber { get; set; }
            public string Address { get; set; }
            public string GenderName { get; set; }
            public string ShiftName { get; set; }
            public string CampusName { get; set; }
            public string ClassName { get; set; }
            public string SectionName { get; set; }
            public string ImagePath { get; set; }

    }
}
