using System.ComponentModel.DataAnnotations;

namespace CarToGo.ViewModel
{
    public class EditRoleViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
        /// <summary>
        /// Gets or sets the list of user names associated with the current context.
        /// </summary>
        public List<string> Users { get; set; } = new();
    }
}
