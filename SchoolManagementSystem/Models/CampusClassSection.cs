using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class CampusClassSection
    {
        
        public int CampusClassSectionId { get; set; }
        [ForeignKey("CampusClass")]
        public int CampusClassId { get; set; }
        [ForeignKey("Section")]
        public int SectionId { get; set; }

        //[ForeignKey("Class")]
        //public int ClassId { get; set; }
        //public virtual Class? Class { get; set; }
        public virtual Section? Section { get; set; }
        public virtual CampusClass? CampusClass { get; set; }


        public virtual ICollection<StudentCampusClassSection> StudentCampusClassSections { get; set; } = new List<StudentCampusClassSection>();
    }
}
