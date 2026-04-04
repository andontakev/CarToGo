using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace CarToGo.Models
{
    [Index(nameof(EGN), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
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
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
    }
}
