using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WebAppStore.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations; // Ensure this is included for IFormFile

namespace WebAppStore.ViewModels
{
    public class AddProductVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        public IEnumerable<IFormFile> Files { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }

        // Navigation Properties
        public virtual Category? Category { get; set; }


        public virtual ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();
    }
}
