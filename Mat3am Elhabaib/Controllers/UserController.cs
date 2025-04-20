using Mat3am_Elhabaib.DataBase;
using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegesterVM regesterVM)
        {
            if (!ModelState.IsValid) return View(regesterVM);

            var user = await userManager.FindByNameAsync(regesterVM.UserName);
            if (user != null)
            {
                TempData["Error"] = "This Name is already in use";
                return View(regesterVM);
            }

            var newUser = new User()
            {
                FullName = regesterVM.UserName,
                Email = regesterVM.Email,
                PhoneNumber = regesterVM.PhoneNumber,
                Location = regesterVM.Address,
            };
            var newUserResponse = await userManager.CreateAsync(newUser, regesterVM.Password);

            if (newUserResponse.Succeeded)
                await userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Items");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, loginVM.RememberMe);
                        return RedirectToAction("Index", "Items");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Items");
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
                    Location = regesterVM.Address,
                    PhoneNumber = regesterVM.PhoneNumber,


                };
                IdentityResult Result = await userManager.CreateAsync(userModel, regesterVM.Password);
                if (Result.Succeeded)
                {
                    RegesterVM admin = new RegesterVM
                    {
                        UserName = regesterVM.UserName,
                        Email = regesterVM.Email,

                        Address = regesterVM.Address,
                        PhoneNumber = regesterVM.PhoneNumber
                    };

                    await userManager.AddToRoleAsync(userModel, "Admin");
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Items");



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
