using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.DTO;
using WebAppStore.Interfaces;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;
        private readonly ICategoryRepository CategoryRepository;
        public ProductController(IProductRepository _ProductRepo, ICategoryRepository _CategoryRepo)
        {

            ProductRepository = _ProductRepo;
            CategoryRepository = _CategoryRepo;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {

            return Ok(ProductRepository.GetAll());
        }
        [HttpGet("{searchTerm}")]
        public IActionResult Search(string searchTerm)
        {

            // Retrieve filtered products from the repository
            var filteredProducts = ProductRepository.search(searchTerm);

            // Pass the filtered products to the Index view
            return Ok(filteredProducts);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Add( AddProductDTO model)
        {
            if (ModelState.IsValid) 
            {
                ProductRepository.Insert(model);
                return Ok("Product Added  successfully!");
            }
            return BadRequest(ModelState);

           
        }
       


        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, AddProductDTO item)
        {
            ProductRepository.Edit(id, item);
            return Ok("Product updated  successfully!");
        
        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            ProductRepository.Delete(id);
            return Ok("Product deleted  successfully!");

        }


    }
}
