using Mat3am_Elhabaib.DataBase;
using Mat3am_Elhabaib.DataBase.Services.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.DataBase.Shop;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("name=DefaultConnectionString"));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                //options.AccessDeniedPath = "/User/AccessDenied"; // ???????
            });
            builder.Services.AddScoped<IItemsService, ItemService>();
            builder.Services.AddScoped<ICategory, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderServices>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IReservationServices, ReservationServices>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
       
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped(sc => Cart.GetShoppingCart(sc));
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
     
            var app = builder.Build();

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
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            AppDbIntailizer.SeedUsersAndRolesAsync(app);

            app.Run();
        }
    }
}
