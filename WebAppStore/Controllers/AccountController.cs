using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.ViewModels;
using WebAppStore.Models;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace WebAppStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid) 
            { // login
                var Result = await signInManager.PasswordSignInAsync(model.Username!,model.Password!,model.RememberMe,false);
                if (Result.Succeeded) 
                {
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid) 
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                   
                };
                IFormFile ProfilePicture = model.ProfilePicture;
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    // Define the path to save the image
                    var filePath = Path.Combine("wwwroot/images/profiles", ProfilePicture.FileName);

                    // Save the image to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePicture.CopyToAsync(stream);
                    }

                    // Save the file path in the user's profile
                    user.ProfilePicture = "/images/profiles/" + ProfilePicture.FileName;
                    await userManager.UpdateAsync(user);
                }

                var result = await userManager.CreateAsync(user,model.Password!);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, false);
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("",error.Description);
                }
            }


            return View(model);
        }

  
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Start", "Home");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {

            var users = await userManager.Users.ToListAsync();

            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // User not found
            }

           
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Users"); 
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction("Users");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Role(RoleVM newrole)
        {


           
            var existingUser = await userManager.FindByIdAsync(newrole.Id);
            if (existingUser == null)
            {
                
                return RedirectToAction("Users");
            }

            
            var currentRoles = await userManager.GetRolesAsync(existingUser);

           
            var removeResult = await userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            if (!removeResult.Succeeded)
            {
                // Handle errors during removal
                foreach (var error in removeResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Users"); 
            }

          
            var addResult = await userManager.AddToRoleAsync(existingUser, newrole.RoleName);
            if (!addResult.Succeeded)
            {
                // Handle errors during role addition
                foreach (var error in addResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Users"); 
            }

            TempData["SuccessMessage"] = "Role User updated successfully!";
            return RedirectToAction("Index", "Home");

        }


        [Authorize]
        public async Task<IActionResult> EditUser(string id)
        {
            // Find the user by their ID
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // User not found
            }
            var role = await userManager.GetRolesAsync(user);
            ViewBag.Role = role.First();

            // Pass the user to the view for editing
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user); 
            }

            var existingUser = await userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return NotFound(); 
            }

          
            existingUser.UserName = user.Email;
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            existingUser.Address = user.Address;

            IFormFile ProfilePicture = user.ProfilePicture;
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                // Define the path to save the image
                var filePath = Path.Combine("wwwroot/images/profiles", ProfilePicture.FileName);

                // Save the image to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(stream);
                }

                // Save the file path in the user's profile
                existingUser.ProfilePicture = "/images/profiles/" + ProfilePicture.FileName;
                await userManager.UpdateAsync(existingUser);
            }

            var result = await userManager.UpdateAsync(existingUser);

            // Handle errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            TempData["SuccessMessage"] = "User updated successfully!";
            return RedirectToAction("Index","Home"); 
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
			var user = await userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound(); // User not found
			}

			return View(user);
        }




   


    }
}
