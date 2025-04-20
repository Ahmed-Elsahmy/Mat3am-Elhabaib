using Mat3am_Elhabaib.DataBase.Services.Impelementation;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mat3am_Elhabaib.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemService itemService;
        private ItemsController(ItemService itemService)
        {
            this.itemService = itemService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await this.itemService.GetAll();
            return View(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dropdowm = await this.itemService.NewItemDropDownAsync();
            ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
            return View(dropdowm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Items items)
        {
            if (!ModelState.IsValid) {
                var dropdowm = await this.itemService.NewItemDropDownAsync();
                ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
                return View(items);
            }
            await this.itemService.CreateItemAsync(items);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await this.itemService.GetItemByIdAsync(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            var response = new Items()
            {
                Description = data.Description,
                Imageurl = data.Imageurl,
                Name
                = data.Name,
                CategoryId = data.CategoryId,
                Price = data.Price,
                Isavailable = data.Isavailable,
            };
            var drop = await this.itemService.NewItemDropDownAsync();
            ViewBag.Category = new SelectList(drop.categories, "Id", "Name");
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Items items)
        {
            if (id != items.Id)
            {
                return View("NOT FOUND");
            }
            if (!ModelState.IsValid)
            {
                var dropdowm = await this.itemService.NewItemDropDownAsync();
                ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
                return View(items);
            }
            await this.itemService.UpdateItemAsync(items);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
         public async Task<IActionResult> Delete(int id)
        {
            var data = await this.itemService.GetItemByIdAsync(id);
            if (data == null) {
                return View("NOT FOUND");
            }
            return View(data);
        }
        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> Deleteitem(int id)
        {
           var data = await this.itemService.GetItemByIdAsync(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            await this.itemService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
