using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class SchoolVM
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = default!;

        public string SchoolLocation { get; set; } = default!;

        public string Email { get; set; } = default!;
        public DateOnly EstablishedYear { get; set; }
        [ForeignKey("SchoolType")]
        public int SchoolTypeId { get; set; }
        public virtual SchoolType? SchoolType { get; set; }
        public string? buildingStringify { get; set; }


        public virtual List<Building> BuildingsList { get; set; } = new List<Building>();
    }
}
