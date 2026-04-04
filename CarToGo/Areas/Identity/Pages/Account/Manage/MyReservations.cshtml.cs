using CarToGo.Data;
using CarToGo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarToGo.Areas.Identity.Pages.Account.Manage
{
    public class MyReservationsModel : PageModel
    {
        private readonly CarToGoContext _context;
        private readonly UserManager<DefaultUser> _userManager;

        public MyReservationsModel(CarToGoContext context, UserManager<DefaultUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Reservation> Reservations { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Reservations = await _context.Reservations
                .Include(r => r.Car)
                .Where(r => r.UserId == user.Id)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Id);

            if (reservation != null && reservation.Status == "Pending")
            {
                reservation.Status = "Canceled";
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
