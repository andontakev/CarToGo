using System.ComponentModel.DataAnnotations;

namespace CarToGo.Models
{
    public class Car
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Brand of the car 
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Model 
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Year of production
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Number of seats in the car
        /// </summary>
        public int Seats { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Price per day for renting the car
        /// </summary>
        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }
    }
}
