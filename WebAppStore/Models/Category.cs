using System.ComponentModel.DataAnnotations;

namespace WebAppStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Icon { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public virtual List<Product> Products { get; set; }
    }
}
