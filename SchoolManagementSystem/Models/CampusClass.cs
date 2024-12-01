using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class CampusClass
    {
        public int CampusClassId { get; set; }
        [ForeignKey("Campus")]
        public int CampusId { get; set; }
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public virtual Campus? Campus { get; set; }
        public virtual Class? Class { get; set; }
        public virtual List<CampusClassSection> CampusClassSections { get; set; } = new List<CampusClassSection>();
    }
}
