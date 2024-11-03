using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IProductRepository ProductRepository;
        private readonly ICategoryRepository CategoryRepository;
        public ProductController( IProductRepository _ProductRepo,ICategoryRepository _CategoryRepo)
        {

            ProductRepository = _ProductRepo;
            CategoryRepository = _CategoryRepo;
        }
       
        public IActionResult Index()
        {

            return View(ProductRepository.GetAll());
        }
       
        public IActionResult Details(int Id)
        {

            return View(ProductRepository.GetById(Id));
        }

        public IActionResult Search(string searchTerm)
        {
            ViewData["CurrentFilter"] = searchTerm;

            // Retrieve filtered products from the repository
            var filteredProducts = ProductRepository.search(searchTerm);

            // Pass the filtered products to the Index view
            return View("Index", filteredProducts);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewBag.categories = CategoryRepository.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Save(AddProductVM model)
        {
            if (!ModelState.IsValid)
            {
               
                return RedirectToAction("Add");
            }

            try
            {
                if (model.CategoryId == 0)
                {
                    
                    ModelState.AddModelError("", " Please, Select Category !!");
                    return View("Add", model);
                }
                ProductRepository.Insert(model);
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error and show an error message)
                ModelState.AddModelError("", "An error occurred while saving the product. Please try again.");
                return View("Add", model);
            }
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int Id)
        {

            // Retrieve the product
            ProductDetailsVM Product = ProductRepository.GetById(Id);
            AddProductVM addProductVM   = new AddProductVM();
            addProductVM.Id = Product.product.Id;
            addProductVM.Name = Product.product.Name;
            addProductVM.Description = Product.product.Description;
            addProductVM.Price = Product.product.Price;
            addProductVM.CategoryId = Product.product.CategoryId;

            ViewBag.categories = CategoryRepository.GetAll();
            if (addProductVM == null)
            {
                return NotFound(); 
            }

            return View(addProductVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, AddProductVM item)
        {
           ProductRepository.Edit(id, item);
            TempData["SuccessMessage"] = "Product updated  successfully!";
            return RedirectToAction("Index");
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ProductRepository.Delete(id);
            TempData["SuccessMessage"] = "Product deleted  successfully!";
            return RedirectToAction("Index");

        }

    }
}
