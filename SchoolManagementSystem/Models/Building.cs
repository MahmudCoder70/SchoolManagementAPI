using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = default!;
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public virtual School ?School { get; set; }

    }
}
