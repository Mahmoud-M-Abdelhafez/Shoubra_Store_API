using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public List<ProductImage> productImages { get; set; }

        public Category category { get; set; }

        

    }
}
