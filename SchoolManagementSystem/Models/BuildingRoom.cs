using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class BuildingRoom
    {
        public int BuildingRoomId { get; set; }
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public int RoomNumber { get; set; }
        public virtual Building ? Building {  get; set; }

    }
}
