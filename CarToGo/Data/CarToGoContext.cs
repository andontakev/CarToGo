using Microsoft.EntityFrameworkCore;

namespace CarToGo.Data
{
    public class CarToGoContext: DbContext
    {
        public CarToGoContext(DbContextOptions<CarToGoContext> options) : base(options)
        {
        }
        
    }
}
