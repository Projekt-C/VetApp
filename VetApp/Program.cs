using Microsoft.EntityFrameworkCore;
using VetApp.Models;
using VetApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace VetApp
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
    
            builder.Services.AddDbContext<PetDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<PetDbContext>();
                


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            var app = builder.Build();
            app.MapRazorPages();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] {"Admin", "User", "Taken"};
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                await CreateAdminUser(services);

            }

            async Task CreateAdminUser(IServiceProvider serviceProvider)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                // Check if the user exists
                var adminEmail = "admin@example.com";
                var adminPassword = "Admin123!.";
                var phone = "123456789";
                var name = "Admin"; 
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    // Create the user
                    var newAdmin = new User
                    {
                        UserName = adminEmail,
                        Name = name,
                        Email = adminEmail,
                        Phone = phone,
                        Password = adminPassword
                    };
                    var result = await userManager.CreateAsync(newAdmin, adminPassword);

                    if (result.Succeeded)
                    {
                        // Assign the Admin role
                        await userManager.AddToRoleAsync(newAdmin, "Admin");
                    }
                }
            }



            app.Run();
        }
    }
}
