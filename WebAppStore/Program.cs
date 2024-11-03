using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppStore.Data;
using WebAppStore.Interfaces;
using WebAppStore.Models;
using WebAppStore.Repository;

namespace WebAppStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("default");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreContextDB>
                (
                options => options.UseSqlServer(connectionString)
                );
            builder.Services.AddIdentity<AppUser,IdentityRole>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreContextDB>().AddDefaultTokenProviders();

            //Custom Service --REgister
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=start}/{ id?}");

            using(var scope = app.Services.CreateScope() )
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] {"Admin","User" };
                foreach( var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string Name = "Admin";
                string email = "Admin@gmail.com";
                string Password = "12345678";
                string Address = "Egypt";
                if(await userManager.FindByEmailAsync(email)==null)
                {
                    var user = new AppUser();
                    user.Name = Name;
                    user.Email = email;
                    user.UserName = email;
                    user.Address = Address;
            

                    await userManager.CreateAsync(user, Password);

                    await userManager.AddToRoleAsync(user, "Admin");

                }

            }

            app.Run();
        }
    }
}
