public class Reservation
{
    public int Id { get; set; }
    public int BoardroomId { get; set; }
    public string UserId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Purpose { get; set; }
    
}