using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppStore.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
    
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
