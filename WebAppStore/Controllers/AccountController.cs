using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppStore.ViewModels;
using WebAppStore.Models;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppStore.DTO;


namespace WebAppStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        
        
        
        //api/Account/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO UserRegister)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = UserRegister.Email;
                user.Name = UserRegister.Username;
                user.UserName = UserRegister.Username;
                IdentityResult result = await userManager.CreateAsync(user, UserRegister.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Created Successfully!");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

       
        
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO UserLogin)
        {
            if (ModelState.IsValid)
            {
                //check
                AppUser userDB = await userManager.FindByNameAsync(UserLogin.Username);
                if (userDB != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userDB, UserLogin.Password);
                    if (found == true)
                    {
                        //Generate Token
                        List<Claim> USerclaims = new List<Claim>();
                        USerclaims.Add(new Claim(ClaimTypes.NameIdentifier, userDB.Id));
                        USerclaims.Add(new Claim(ClaimTypes.Name, userDB.UserName));
                        USerclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


                        var roles = await userManager.GetRolesAsync(userDB);
                        foreach (var rolename in roles)
                        {
                            USerclaims.Add(new Claim(ClaimTypes.Role, rolename));
                        }
                        var SignInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
                        SigningCredentials signingCreds =
                            new SigningCredentials(SignInkey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: configuration["JWT:IssuerIP"],
                            audience: configuration["JWT:AudienceIP"],
                            expires: DateTime.Now.AddDays(1),
                            claims: USerclaims,
                            signingCredentials: signingCreds


                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = DateTime.Now.AddDays(1)

                        });
                    }
                    ModelState.AddModelError("USername", "Username OR Password Invaild");


                }


            }
            return BadRequest(ModelState);

        }

       
        
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
List<UserDetailsDTO> Users = new List<UserDetailsDTO>();
            var users = await userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                UserDetailsDTO UserDetails = new UserDetailsDTO();
                UserDetails.Id = user.Id;
                UserDetails.Name = user.Name;
                UserDetails.Email = user.Email;
                Users.Add(UserDetails);
            }
            return Ok(Users);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User not found"); // User not found
            }


            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("User Deleted Successfully!");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
          
            return BadRequest(ModelState);
        }
    }
}
