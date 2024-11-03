using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public IActionResult Add([FromBody]AddProductVM model)
        {
            if (ModelState.IsValid) 
            {
                ProductRepository.Insert(model);
                return Created();
            }
            return BadRequest(ModelState);

           
        }

    }
}
