using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class CategoryDetailsVM
    {
        public List<Product> products { get; set; }
        public List<ProductImage> productsImages { get; set; }

        public Category category { get; set; }
    }
}
