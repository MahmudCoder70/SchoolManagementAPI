using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class SubjectVM
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = default!;
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        
    }
}
