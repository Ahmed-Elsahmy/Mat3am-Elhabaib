using Mat3am_Elhabaib.DataBase;
using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Mat3am_Elhabaib.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private AppDbContext AppDbContext;
        public UserController(UserManager<User> manager, SignInManager<User> signIn, AppDbContext appDb)
        {
            userManager = manager;
            signInManager = signIn;
            AppDbContext = appDb;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var data = await AppDbContext.users.ToListAsync();
            return View(data);
        }
        public IActionResult Register()
        {
            return View(new RegesterVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegesterVM regesterVM)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = regesterVM.UserName;
                user.Email = regesterVM.Email;
                user.PasswordHash = regesterVM.Password;
                user.PhoneNumber = regesterVM.PhoneNumber;
                user.Location = regesterVM.Location;
                IdentityResult Result = await userManager.CreateAsync(user, regesterVM.Password);
                if (Result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");


                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                }

            }
            return View(regesterVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                User userModel = await userManager.FindByNameAsync(loginVM.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, loginVM.Password);
                    if (found == true)
                    {
                        await signInManager.SignInAsync(userModel, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "user name or password are wrong ");


            }
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login" , "User");
        }
  
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View(new RegesterVM());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegesterVM regesterVM)
        {
            if (ModelState.IsValid)
            {
                User userModel = new User
                {
                    UserName = regesterVM.UserName,
                    Email = regesterVM.Email,
                    Location = regesterVM.Location,
                    PhoneNumber = regesterVM.PhoneNumber,


                };
                IdentityResult Result = await userManager.CreateAsync(userModel, regesterVM.Password);
                if (Result.Succeeded)
                {
                    RegesterVM admin = new RegesterVM
                    {
                        UserName = regesterVM.UserName,
                        Email = regesterVM.Email,
                        Location = regesterVM.Location,
                        PhoneNumber = regesterVM.PhoneNumber
                    };

                    await userManager.AddToRoleAsync(userModel, UserRoles.Admin);
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");



                }
                else
                {
                    // If user creation failed, add errors to the model state
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }



            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Log the error message
                }
            }
            return View(regesterVM);
        }
    }

}
