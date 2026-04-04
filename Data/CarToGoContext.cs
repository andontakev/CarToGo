using CarToGo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarToGo.Data
{
    public class CarToGoContext: IdentityDbContext<DefaultUser>
    {
        /// <summary>
        /// Initializes a new instance of the CarToGoContext class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext. Must not be null.</param>
        public CarToGoContext(DbContextOptions<CarToGoContext> options) : base(options)
        {
        }
        /// <summary>
        /// Configures the entity framework model for the context, including relationships and constraints between
        /// entities.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        /// <summary>
        /// Gets or sets the collection of cars in the database context.
        /// </summary>
        public DbSet<Car> Cars { get; set; }
        /// <summary>
        /// Gets or sets the collection of reservations in the database context.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

    }
}
