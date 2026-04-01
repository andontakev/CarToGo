using CarToGo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarToGo.Data
{
    public class CarToGoContext: IdentityDbContext<DefaultUser>
    {
        public CarToGoContext(DbContextOptions<CarToGoContext> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
