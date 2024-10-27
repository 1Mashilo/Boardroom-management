using System.ComponentModel.DataAnnotations.Schema;


namespace boardroom_management.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        
        [Display(Name = "Booking Name")]
        public string BookingName { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Booking Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingDay { get; set; }

        [Display(Name = "Booking Start")]
        public TimeSpan BookingStart { get; set; }

        [Display(Name = "Booking End")]
        public TimeSpan BookingEnd { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}