namespace CarToGo.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the current request.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Shows whether the RequestId should be displayed.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
