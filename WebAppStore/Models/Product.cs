using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("catID")]
        public int CategoryId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public virtual Category? catID { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductImage> Images { get; set; } = new HashSet<ProductImage>();

    }
}
