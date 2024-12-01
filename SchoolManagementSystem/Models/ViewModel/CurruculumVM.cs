using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class CurruculumVM
    {
        public int CurriculumId { get; set; }
        public string CurriculumName { get; set; } = default!;
        public string Description { get; set; } = default!;

        //public string? CampusStringify { get; set; }
        //public string? SubjectStringify { get; set; }

        public CurruculumVM(){}
        public CurruculumVM(int curriculumId, string curriculumName, string description, List<Campus> campuses, List<Subject> subjects) :this() 
        {
            CurriculumId = curriculumId;
            CurriculumName = curriculumName;
            Description = description;
            Campuses = campuses;
            Subjects = subjects;
        }

        public List<Campus> Campuses { get; set; } = new List<Campus>();
        public List<Subject> Subjects { get; set; } = new List<Subject>(); 

    }
}
