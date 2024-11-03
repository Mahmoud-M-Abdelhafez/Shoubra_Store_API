using Microsoft.AspNetCore.Mvc;
using WebAppStore.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebAppStore.ViewModels;
using WebAppStore.Interfaces;

namespace WebAppStore.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        private readonly UserManager<AppUser> userManager;
        private readonly IProductRepository ProductRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IReviewRepository reviewRepository;

        public HomeController(UserManager<AppUser> userManager,IProductRepository _ProductRepo, ICategoryRepository _CategoryRepo , IReviewRepository _ReviewRepo)
        {
            this.userManager = userManager;
            ProductRepository = _ProductRepo;
            CategoryRepository = _CategoryRepo;
            reviewRepository = _ReviewRepo;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.UsersCount = await userManager.Users.CountAsync();

            HomeVM homeVM = new HomeVM();
             
            var Users = await userManager.Users.ToListAsync();
            homeVM.Users = Users;
            homeVM.NoOfUsers = Users.Count;
            homeVM.categories = CategoryRepository.GetAll();
            homeVM.products = ProductRepository.GetAll();
            homeVM.reviews = reviewRepository.GetAll();
            homeVM.NoOfCategories = homeVM.categories.Count();
            homeVM.NoOfProducts = homeVM.products.products.Count();
            

            return View(homeVM);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Start()
        {
            var Reviews = reviewRepository.GetAll();
            var users = await userManager.Users.ToListAsync();
            ViewBag.Users = users;

            return View(Reviews);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
