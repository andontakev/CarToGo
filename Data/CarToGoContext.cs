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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
