using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Mat3am_Elhabaib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory category;
        public CategoryController(ICategory category)
        {
            this.category = category;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await this.category.GetAll();
            return View(data);
        }


        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                await this.category.Add(category);
            }
            catch (Exception ex)
            {
                // Log or debug here
                ModelState.AddModelError("", "Error adding category: " + ex.Message);
                return View(category);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await this.category.GetCategorybyIdAsync(id);
            return View(data);
        }
    }
}
