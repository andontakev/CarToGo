using System.ComponentModel.DataAnnotations;

namespace CarToGo.ViewModel
{
    public class AddRoleViewModel
    {
        /// <summary>
        /// Gets or sets the name of the role associated with the user or entity.
        /// </summary>
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
