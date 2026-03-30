namespace CarToGo.Models
{
    public class Reservation
    {
        /// <summary>
        /// Id of the car
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Id of the user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Start date of the reservation
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// End date of the reservation
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Car navigation property
        /// </summary>
        public Car Car { get; set; }
        /// <summary>
        /// User navigation property
        /// </summary>
        public DefaultUser User { get; set; }
    }
}
