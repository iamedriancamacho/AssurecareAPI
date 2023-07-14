using System.ComponentModel.DataAnnotations;

namespace AssurecareAPI.Models
{
    public class AssessmentModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please provide your rating.")]
        [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5.")]
        public int Rating { get; set; }
    }
}