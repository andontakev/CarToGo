namespace CarToGo.ViewModel
{
    public class AvailableCarViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the car.
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Gets or sets the brand name associated with the item.
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Gets or sets the model name associated with the item.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Gets or sets the year component.
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the number of seats available.
        /// </summary>
        public int Seats { get; set; }
        /// <summary>
        /// Price per day for renting the car
        /// </summary>
        public decimal PricePerDay { get; set; }
        /// <summary>
        /// Gets or sets the total price for the associated order or transaction.
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Gets or sets the start date for the associated event or operation.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date for the period or event.
        /// </summary>
        public DateTime EndDate { get; set; }
    }

}
