using System.ComponentModel.DataAnnotations.Schema;

namespace boardroom_management.Models
{
    public class Room
    {
        public int RoomID { get; set; }

        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        [NotMapped]
        public ICollection<Booking> Bookings { get; set; }
    }
}
