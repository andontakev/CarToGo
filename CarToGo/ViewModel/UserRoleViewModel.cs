namespace CarToGo.ViewModel
{
    public class UserRoleViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the object.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the role associated with the user or entity.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the item is selected.
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
