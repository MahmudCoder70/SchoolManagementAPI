using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models.ViewModel
{
    public class BuildingRoomVM
    {
        public int BuildingRoomId { get; set; }
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public int RoomNumber { get; set; }
    }
}
