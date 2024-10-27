
using System.Globalization;

namespace boardroom_management.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext db;

        public BookingsController(ApplicationDbContext context)
        {
            db = context;
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        }

        public ActionResult Index()
        {
            ViewBag.RoomList = db.Rooms.ToList();
            return View(db.Bookings.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,BookingName,OwnerName,BookingDay,BookingStart,BookingEnd")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ID,BookingName,OwnerName,BookingDay,BookingStart,BookingEnd")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<string> GetAvailableStartHourFromDay(DateTime day, TimeSpan startHour, int roomID)
        {
            List<string> availableHourList = new List<string>();
            var bookings = db.Bookings.Where(b => b.BookingDay == day && b.RoomId == roomID).ToList();

            for (TimeSpan ts = startHour; ts < new TimeSpan(17, 0, 0); ts += TimeSpan.FromMinutes(30))
            {
                if (!bookings.Any(b => IsTimeBetween(ts, b.BookingStart, b.BookingEnd)))
                    availableHourList.Add(ts.ToString("hh\\:mm"));
            }

            return availableHourList;
        }

        public List<string> GetAvailableEndHourFromDay(DateTime day, int roomID)
        {
            List<string> availableHourList = new List<string>();
            var bookings = db.Bookings.Where(b => b.BookingDay == day && b.RoomId == roomID).ToList();

            for (TimeSpan ts = new TimeSpan(9, 0, 0); ts < new TimeSpan(17, 0, 0); ts += TimeSpan.FromMinutes(30))
            {
                if (!bookings.Any(b => IsTimeBetween(ts, b.BookingStart, b.BookingEnd)))
                    availableHourList.Add(ts.ToString("hh\\:mm"));
            }

            return availableHourList;
        }

        private bool IsTimeBetween(TimeSpan time, TimeSpan start, TimeSpan end)
        {
            return start <= time && time <= end;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
