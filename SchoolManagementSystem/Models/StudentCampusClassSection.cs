using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class StudentCampusClassSection
    {
        public int StudentCampusClassSectionId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("CampusClassSection")]
        public int CampusClassSectionId { get; set; }
 
        public virtual Student? Student { get; set; }
        //public virtual Class? Class { get; set; }
        public virtual CampusClassSection? CampusClassSection { get; set; }
    }
}
