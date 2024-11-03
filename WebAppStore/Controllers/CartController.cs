using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository _CartRepo)
        {
            cartRepository = _CartRepo;
        }
        public IActionResult Index(string id)
        {
            
            return View(cartRepository.GetAll(id));
        }
        [HttpPost]
        public IActionResult Save(AddCartVM model)
        {

          

            cartRepository.Insert(model);
            TempData["SuccessMessage"] = "Product added to cart successfully!";
 

            return RedirectToAction("Index","Product");

           
        }


        public IActionResult Edit(int id)
        {
           
            return View(cartRepository.GetById(id));
        }
        [HttpPost]
        public IActionResult Update(int id,AddCartVM model)
        {
            string UserId = model.UserId;
            cartRepository.Edit(id,model);
           
            return RedirectToAction("Index", new { id = UserId });
        }


        public IActionResult Delete(int id)
        {
           string UserId = cartRepository.Delete(id);
            return RedirectToAction("Index", new { id = UserId });

        }

    }
}
