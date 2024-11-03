using WebAppStore.Data;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContextDB db;
        public CategoryRepository(StoreContextDB context)
        {
            db = context;
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public CategoryDetailsVM GetById(int id)
        {
            CategoryDetailsVM categoryDetailsVM = new CategoryDetailsVM();
            categoryDetailsVM.products = db.Products.Where(p => p.CategoryId == id).ToList();
            categoryDetailsVM.productsImages = db.ProductImages.ToList();
            categoryDetailsVM.category = db.Categories.SingleOrDefault(c => c.Id == id);
            return (categoryDetailsVM);   
        }
 



        public void Insert(AddCategoryVM item)
        {
            var cat = new Category
            {
                Name = item.Name,
                Description = item.Description,
                Icon = item.Icon
            };

            db.Categories.Add(cat);
            db.SaveChanges();
        }


        public void Edit(int id, Category Ct)
        {
            CategoryDetailsVM cat = new CategoryDetailsVM();
            cat = GetById(id);
      
            cat.category.Name = Ct.Name;
            cat.category.Description = Ct.Description;
            cat.category.Icon = Ct.Icon;
            db.SaveChanges();
        }



        public void Delete(int id)
        {
            CategoryDetailsVM cat = new CategoryDetailsVM();
            cat = GetById(id);
            db.Categories.Remove(cat.category);
            db.SaveChanges();
        }
    }
}
