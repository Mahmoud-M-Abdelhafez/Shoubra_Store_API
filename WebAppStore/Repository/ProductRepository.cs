using Microsoft.EntityFrameworkCore;
using WebAppStore.Data;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContextDB db;
        ProductDetailsVM productDetailsVM = new ProductDetailsVM();
        ProductsWithImagesVM productsWithImagesVM = new ProductsWithImagesVM();
        public ProductRepository(StoreContextDB context)
        {
            db = context;
        }

        

        public ProductsWithImagesVM GetAll()
        {
           
            productsWithImagesVM.products = db.Products.ToList();
            productsWithImagesVM.images = db.ProductImages.ToList();
            return productsWithImagesVM;
        }

        public ProductDetailsVM GetById(int id)
        {
            ProductDetailsVM ProductDetailsVM = new ProductDetailsVM();
            ProductDetailsVM.product = db.Products.SingleOrDefault(p => p.Id == id);
            ProductDetailsVM.productImages= db.ProductImages.Where(s=>s.ProductId == id).ToList();
            ProductDetailsVM.category = db.Categories.Where(c=>c.Id== ProductDetailsVM.product.CategoryId).Single();
            return (ProductDetailsVM);
        }

        public async void Insert(AddProductVM item)
        {
            // Create and save the product
            var product = new Product
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId
            };
            db.Products.Add(product);
            db.SaveChanges();
            if (item.Files != null && item.Files.Any())
            {
                foreach (var file in item.Files)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Generate a unique file name to prevent overwriting
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        var filePath = Path.Combine("/images/products", uniqueFileName);
                        var filePathfolder = Path.Combine("wwwroot/images/products", uniqueFileName);

                        // Save the file to the specified path
                        using (var stream = new FileStream(filePathfolder, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Add image information to the database
                        var productImage = new ProductImage
                        {
                            Path = filePath,
                            ProductId = product.Id
                        };

                        db.ProductImages.Add(productImage);
                    }
                }

                db.SaveChanges();
            }
        }

        public void Edit(int id, AddProductVM item)
        {
            
            productDetailsVM = GetById(id);
            
            productDetailsVM.product.Name = item.Name;
            productDetailsVM.product.Description = item.Description;
            productDetailsVM.product.Price = item.Price;
            productDetailsVM.product.CategoryId = item.CategoryId;
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            
            productDetailsVM = GetById(id);
            db.Products.Remove(productDetailsVM.product);
            db.SaveChanges();
        }

        public ProductsWithImagesVM search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsWithImagesVM.products = db.Products.Where(p => p.Name.Contains(searchTerm) ||
                                               p.Description.Contains(searchTerm)).ToList();
                productsWithImagesVM.images = db.ProductImages.ToList();
            }

            return productsWithImagesVM;
        }
    }
}
