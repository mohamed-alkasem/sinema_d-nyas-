using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sinema_Dunyasi.Data;
using Sinema_Dunyasi.Models;
using Sinema_Dunyasi.Repositories.Abstract;
using Sinema_Dunyasi.Repositories.Implementation;

namespace Sinema_Dunyasi
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache(); 
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });

            //-------- Dependency Injection 13 ------------
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IMovieService, MovieService>();


            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            // Configure Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();


            var app = builder.Build();


            // Rolleri ve admin kullan?c?y? ekleme----------------------------------
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                Task.Run(async () =>
                {
                    // Admin rolü ekle
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Varsay?lan admin kullan?c?y? olu?tur
                    string Email = "2112721307";
                    string adminPassword = "Admin123"; // Güçlü bir ?ifre belirleyin

                    if (await userManager.FindByNameAsync(Email) == null)
                    {
                        var adminUser = new AppUser
                        {
                            UserName = Email,
                            Email = Email,
                            FullName = "Sistem Yöneticisi",
                        };

                        var result = await userManager.CreateAsync(adminUser, adminPassword);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser, "Admin");
                        }
                    }
                }).GetAwaiter().GetResult();
            }




            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();


            // --------20 Authentication   --------21 Authorization
            app.UseAuthorization();

            app.UseAuthentication();



            app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapDefaultControllerRoute();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
