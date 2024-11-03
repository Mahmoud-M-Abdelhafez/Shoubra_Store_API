using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class AddReviewVM
    {
        public int Id { get; set; }
		[Range(1, 5, ErrorMessage = "Please select a star rating between 1 and 5.")]
		[Required]
        public int stars { get; set; }

        [Required]
        public string Description { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public virtual AppUser? user { get; set; }
    }
}
