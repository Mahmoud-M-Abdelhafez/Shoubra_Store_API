using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("userId")]
        public string UserId { get; set; }
        [ForeignKey("prodId")]
        public int ProductId { get; set; }
        public int Qty { get; set; }

        //Navigation Properties

        public virtual Product prodId { get; set; }
        public virtual AppUser userId { get; set; }
    }
}
