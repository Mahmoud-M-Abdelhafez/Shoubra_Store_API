using System.ComponentModel.DataAnnotations.Schema;
using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class AddCartVM
    {
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
