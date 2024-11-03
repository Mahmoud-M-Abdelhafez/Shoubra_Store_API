using WebAppStore.Models;

namespace WebAppStore.ViewModels
{
    public class HomeVM
    {
        public List<Category> categories {  get; set; }
        public List<Review> reviews {  get; set; }
        public List<AppUser> Users {  get; set; }
        public ProductsWithImagesVM products { get; set; }

   

        public int NoOfProducts { get; set; }
        public int NoOfCategories { get; set; }
        public int NoOfUsers { get; set; }
       


    }
}
