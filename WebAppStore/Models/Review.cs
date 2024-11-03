using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppStore.Models
{
    public class Review
    {
        public int Id { get; set; }
		
		[Required]
        public int stars { get; set; }
        
        [Required]
        public string Description { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public virtual AppUser? user { get; set; }

    }
}
