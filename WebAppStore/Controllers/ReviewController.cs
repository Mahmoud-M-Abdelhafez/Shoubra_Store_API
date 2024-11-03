using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    public class ReviewController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IReviewRepository reviewRepository;
        public ReviewController( UserManager<AppUser> userManager , IReviewRepository _ReviewRepo)
        {
            reviewRepository = _ReviewRepo;
            this.userManager = userManager;
        }
        private readonly IProductRepository ProductRepository;
       

        public async Task<IActionResult> Index()
        {
            var Reviews = reviewRepository.GetAll();
            var users = await userManager.Users.ToListAsync();
            ViewBag.Users=users;

            return View(Reviews);
        }

        public async  Task<IActionResult> Add(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Save(AddReviewVM model)
        {
            if (!ModelState.IsValid)
            {
                // Return the view with the model to show validation errors
                return View("Add", model);
            }
            reviewRepository.Insert(model);
            TempData["SuccessMessage"] = "Review added successfully!";
            return RedirectToAction("Index");

        }




        public IActionResult Edit(int Id)
        {

            Review review = reviewRepository.GetById(Id);

            if (review == null)
            {
                return NotFound(); 
            }



            return View(review);
        }

        [HttpPost]
        public IActionResult Update(int Id, AddReviewVM model)
        {
            reviewRepository.Edit(Id, model);
            TempData["SuccessMessage"] = "Review updated successfully!";
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            reviewRepository.Delete(id);
            TempData["SuccessMessage"] = "Review deleted successfully!";
            return RedirectToAction("Index");

        }
    }
}
