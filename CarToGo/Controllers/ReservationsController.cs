using CarToGo.Data;
using CarToGo.Models;
using CarToGo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarToGo.Controllers;

        [Authorize]
        public class ReservationsController : Controller
        {
            private readonly CarToGoContext _context;
            private readonly UserManager<DefaultUser> _userManager;

            public ReservationsController(CarToGoContext context, UserManager<DefaultUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            [HttpGet]
            public IActionResult New()
            {
                return View();
            }

            
            [HttpPost]
            public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
            {
                if (startDate.Date < DateTime.Today)
                {
                    ModelState.AddModelError("", "Start date cannot be in the past.");
                    return View("New");
                }

                if (endDate <= startDate)
                {
                    ModelState.AddModelError("", "End date must be after start date.");
                    return View("New");
                }

                TempData["StartDate"] = startDate.ToString("yyyy-MM-dd");
                TempData["EndDate"] = endDate.ToString("yyyy-MM-dd");

                return await BuildAvailableCarsView(startDate, endDate);
            }

            [HttpGet]
            public async Task<IActionResult> AvailableCarsByDates(string startDate, string endDate)
            {
                if (!DateTime.TryParse(startDate, out var start) || !DateTime.TryParse(endDate, out var end))
                {
                    return RedirectToAction("New");
                }

                return await BuildAvailableCarsView(start, end);
            }

            private async Task<IActionResult> BuildAvailableCarsView(DateTime startDate, DateTime endDate)
            {
                var availableCars = await _context.Cars
                    .Where(c => !_context.Reservations
                        .Any(r => r.Status != "Canceled" && 
                                  r.CarId == c.Id &&
                                  r.StartDate < endDate && 
                                  startDate < r.EndDate))
                    .ToListAsync();

                var model = availableCars.Select(c => new AvailableCarViewModel
                {
                    CarId = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Seats = c.Seats,
                    PricePerDay = c.PricePerDay,
                    TotalPrice = (decimal)(endDate - startDate).TotalDays * c.PricePerDay,
                    StartDate = startDate,
                    EndDate = endDate
                }).ToList();

                return View("AvailableCars", model);
            }

            [HttpGet]
            public IActionResult AvailableCarsBack()
            {
                var startDateStr = TempData["StartDate"]?.ToString();
                var endDateStr = TempData["EndDate"]?.ToString();

                if (string.IsNullOrEmpty(startDateStr) || string.IsNullOrEmpty(endDateStr))
                {
                    return RedirectToAction("New");
                }

                return RedirectToAction("AvailableCarsByDates", new { startDate = startDateStr, endDate = endDateStr });
            }

            
            [HttpPost]
            public async Task<IActionResult> MakeReservation(int carId, DateTime startDate, DateTime endDate)
            {
                var user = await _userManager.GetUserAsync(User);

                // Проверка за припокриване (excluding cancelled reservations)
                bool overlaps = await _context.Reservations
                    .AnyAsync(r => r.CarId == carId &&
                                   r.Status != "Canceled" &&
                                   r.StartDate < endDate &&
                                   startDate < r.EndDate);

                if (overlaps)
                {
                    TempData["Error"] = "This car is already reserved for the selected period.";
                    return RedirectToAction("New");
                }

                var car = await _context.Cars.FindAsync(carId);

                var reservation = new Reservation
                {
                    CarId = carId,
                    UserId = user.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = "Pending"
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyReservations");
            }

      
            [HttpGet]
            public async Task<IActionResult> MyReservations()
            {
                var user = await _userManager.GetUserAsync(User);

                var reservations = await _context.Reservations
                    .Include(r => r.Car)
                    .Where(r => r.UserId == user.Id)
                    .OrderByDescending(r => r.StartDate)
                    .ToListAsync();

                return View(reservations);
            }

            [HttpPost]
            public async Task<IActionResult> Cancel(int id)
            {
                var user = await _userManager.GetUserAsync(User);
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

                if (reservation != null && reservation.Status == "Pending")
                {
                    reservation.Status = "Canceled";
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("MyReservations");
            }

           
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Manage()
            {
                var reservations = await _context.Reservations
                    .Include(r => r.Car)
                    .Include(r => r.User)
                    .OrderBy(r => r.Status)
                    .ToListAsync();

                return View(reservations);
            }

          
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Approve(int id)
            {
                var reservation = await _context.Reservations.FindAsync(id);

                if (reservation != null)
                {
                    reservation.Status = "Approved";
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Manage");
            }

            
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Reject(int id)
            {
                var reservation = await _context.Reservations.FindAsync(id);

                if (reservation != null)
                {
                    reservation.Status = "Rejected";
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Manage");
            }
        }
    



