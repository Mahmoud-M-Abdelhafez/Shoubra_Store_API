using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository _CartRepo)
        {
            cartRepository = _CartRepo;
        }
        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            
            return Ok(cartRepository.GetAll(id));
        }
        [HttpPost]
        public IActionResult Save(AddCartVM model)
        {

          

            cartRepository.Insert(model);
          
 

            return Ok("Product added to cart successfully!");

           
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id,AddCartVM model)
        {
            string UserId = model.UserId;
            cartRepository.Edit(id,model);
           
            return Ok("Cart Updated successfully!");
        }


        //public IActionResult Delete(int id)
        //{
        //   string UserId = cartRepository.Delete(id);
        //    return RedirectToAction("Index", new { id = UserId });

        //}

    }
}
