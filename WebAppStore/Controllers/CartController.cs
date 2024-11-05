using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppStore.DTO;
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
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(cartRepository.GetAll(userId));
        }
        [HttpPost]
        [Authorize]
        public IActionResult Save(FromUserCartDTO model)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            AddCartDTO addCartDTO = new AddCartDTO();
            addCartDTO.Userid = userId;
            addCartDTO.Qty= model.Qty;
            addCartDTO.ProductId = model.ProductId;
            cartRepository.Insert(addCartDTO);
          
 

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
