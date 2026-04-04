namespace CarToGo.ViewModel
{
    public class AvailableCarViewModel
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
