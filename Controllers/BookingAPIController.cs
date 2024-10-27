namespace boardroom_management.Controllers
{
    public class BookingAPIController : Controller
    {
        private readonly ApplicationDbContext db;

        public BookingAPIController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/BookingAPI
        public IEnumerable<Booking> Get()
        {
            return db.Bookings.ToList();
        }

        // GET: api/BookingAPI/5
        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        // POST: api/BookingAPI
        public ActionResult Post(Booking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Bookings.Add(booking);
            db.SaveChanges();

            return Ok();
        }

        // PUT: api/BookingAPI/5
        public ActionResult Put(Booking booking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/BookingAPI/5
        public ActionResult Delete(int id)
        {
            var booking = db.Bookings.Find(id);
            if (booking == null)
                return NotFound();

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok();
        }

        [ActionName("GetBookingByDay")]
        [HttpPost]
        public IEnumerable<Booking> GetBookingByDay([FromBody] string bookingDay)
        {
            if (string.IsNullOrEmpty(bookingDay))
                return null;

            DateTime dt = DateTime.Parse(bookingDay);
            return db.Bookings.Where(c => c.BookingDay == dt).ToList();
        }

        [ActionName("GetBookingByRoomId")]
        [HttpPost]
        public IEnumerable<Booking> GetBookingByRoomId([FromBody] int roomId)
        {
            return db.Bookings.Where(c => c.RoomId == roomId);
        }
    }
}
