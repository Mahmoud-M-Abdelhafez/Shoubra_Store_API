using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class CartProductsVM
    {
        public List<Product> products { get; set; }
        public List<ProductImage> images { get; set; }
        public List<Cart> carts { get; set; }

    }
}
