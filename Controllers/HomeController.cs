using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using boardroom_management.Models;
using System.Linq;
using System.Threading.Tasks;

namespace boardroom_management.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = _userManager.GetUserId(User);
                var userName = _userManager.GetUserName(User);

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userId))
                {
                    // Invalid user, sign out
                    return await SignOut();
                }

                ViewBag.UserName = userName;
                return RedirectToAction("Agenda");
            }
            return RedirectToAction("SignIn");
        }

        public IActionResult Agenda()
        {
            var bookings = _db.Bookings
                .OrderBy(b => b.BookingStart)
                .ThenBy(b => b.RoomId)
                .ThenBy(b => b.BookingDay)
                .ToList();
            return View(bookings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(_db.Rooms.ToList());
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            _db.Bookings.Add(booking);
            _db.SaveChanges();

            var requestBody = new BaseEventRequestBody(
                booking.OwnerName + ": " + booking.BookingName,
                booking.BookingDay.Add(booking.BookingStart).ToUniversalTime().ToLongTimeString(),
                booking.BookingDay.Add(booking.BookingEnd).ToUniversalTime().AddHours(3).ToLongTimeString(),
                "body",
                _db.Rooms.FirstOrDefault(r => r.RoomID == booking.RoomId)?.RoomName,
                booking.OwnerName
            );

            var status = GraphAPILib.Helpers.AddEvent(HttpContext, requestBody).Result;

            return View("Agenda");
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(string description)
        {
            var requestBody = new BaseEventRequestBody(
                "MySubject",
                DateTime.Now.ToUniversalTime().ToLongTimeString(),
                DateTime.Now.ToUniversalTime().AddHours(3).ToLongTimeString(),
                "body",
                "Conf. Room",
                "Jean-Michel"
            );

            var status = await GraphAPILib.Helpers.AddEvent(HttpContext, requestBody);
            return status ? RedirectToAction("Index") : RedirectToAction("Error", "Home");
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "OpenIdConnect");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public JsonResult GetAvailableEndHourFromDay(string dayString, string startHourString, int roomID)
        {
            var day = DateTime.Parse(dayString);
            var startHour = TimeSpan.Parse(startHourString);

            var availableHourList = new List<string>();

            var bookings = _db.Bookings
                .Where(b => b.BookingDay == day && b.Room.RoomID == roomID)
                .ToList();

            for (var ts = startHour + TimeSpan.FromMinutes(30); ts <= new TimeSpan(17, 0, 0); ts += TimeSpan.FromMinutes(30))
            {
                if (bookings.Any(b => b.BookingStart < ts && b.BookingEnd >= ts))
                    return Json(availableHourList);

                availableHourList.Add(ts.ToString(@"hh\:mm"));
            }

            return Json(availableHourList);
        }

        public JsonResult GetAvailableStartHourFromDay(string dayString, int roomID)
        {
            var day = DateTime.Parse(dayString);
            var availableHourList = new List<string>();

            var bookings = _db.Bookings
                .Where(b => b.BookingDay == day && b.Room.RoomID == roomID)
                .ToList();

            for (var ts = new TimeSpan(9, 0, 0); ts < new TimeSpan(17, 0, 0); ts += TimeSpan.FromMinutes(30))
            {
                if (!bookings.Any(b => b.BookingStart <= ts && b.BookingEnd > ts))
                    availableHourList.Add(ts.ToString(@"hh\:mm"));
            }

            return Json(availableHourList);
        }
    }
}
