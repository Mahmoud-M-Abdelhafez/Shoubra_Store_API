using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IProductRepository ProductRepository;
        private readonly ICategoryRepository CategoryRepository;
        public CategoryController(IProductRepository _ProductRepo, ICategoryRepository _CategoryRepo)
        {

            ProductRepository = _ProductRepo;
            CategoryRepository = _CategoryRepo;
        }
        [Authorize]
        public IActionResult Index()
        {
            
            return View(CategoryRepository.GetAll());
        }



        [Authorize]
        public IActionResult Details(int Id)
        {
            return View(CategoryRepository.GetById(Id));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
           public IActionResult Save(AddCategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return the view with the current model to show validation errors
                return View(model);
            }

            try
            {
               CategoryRepository.Insert(model);

                TempData["SuccessMessage"] = "Category added successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while saving the category. Please try again.");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            CategoryDetailsVM cat = new CategoryDetailsVM();
            cat = CategoryRepository.GetById(id);
            if (cat.category == null)
            {
                return NotFound();
            }
            return View(cat.category);
        }
        [HttpPost]
     
        public IActionResult Update(int id , Category Cat)
        {
            CategoryRepository.Edit(id, Cat);
            TempData["SuccessMessage"] = "Category updated successfully!";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            CategoryRepository.Delete(id);
            TempData["SuccessMessage"] = "Category deleted successfully!";
            
            return RedirectToAction("Index");
       
        }



    }
}
