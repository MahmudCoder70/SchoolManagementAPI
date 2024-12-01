using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = default!;

        public string SchoolLocation { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string EstablishedYear { get; set; }
        [ForeignKey("SchoolType")]
        public int SchoolTypeId { get; set; }
        public virtual SchoolType? SchoolType { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }= new List<Building>();


    }
}
