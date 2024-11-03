using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.Interfaces;
using WebAppStore.ViewModels;

namespace WebAppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;
        private readonly ICategoryRepository CategoryRepository;
        public CategoryController(IProductRepository _ProductRepo, ICategoryRepository _CategoryRepo)
        {

            ProductRepository = _ProductRepo;
            CategoryRepository = _CategoryRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return Ok(CategoryRepository.GetAll());
        }
        [HttpPost]
        public IActionResult Save(AddCategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, return the view with the current model to show validation errors
                return BadRequest(ModelState);
            }

            try
            {
                CategoryRepository.Insert(model);
                return Created();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "An error occurred while saving the category. Please try again.");
                return BadRequest(ModelState);
            }
        }


    }
}
