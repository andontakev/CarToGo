using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarToGo.Models
{
    public class Reservation
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Id of the car
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Id of the user
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Start date of the reservation
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// End date of the reservation
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Car navigation property
        /// </summary>
        public Car? Car { get; set; }
        /// <summary>
        /// User navigation property
        /// </summary>
        [ForeignKey("UserId")]
        public DefaultUser? User { get; set; }
    }
}
