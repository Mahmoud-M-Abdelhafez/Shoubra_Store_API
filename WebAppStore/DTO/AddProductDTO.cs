using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebAppStore.Models;

namespace WebAppStore.DTO
{
    public class AddProductDTO
    {
        
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
