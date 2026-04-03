using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarToGo.Models
{
    public class DefaultUser: IdentityUser
    {
        /// <summary>
        /// First name of the user
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        /// <summary>
        /// EGN
        /// </summary>
        [RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be exactly 10 digits.")]
        public string EGN { get; set; }
        /// <summary>
        /// Phone number of the user
        /// </summary>
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }
    }
}
