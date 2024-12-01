using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class BuildingVM
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = default!;
        [ForeignKey("School")]
        public int SchoolId { get; set; }
    }
}
