using Mat3am_Elhabaib.DataBase;
using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Services.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mat3am_Elhabaib.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemsService service;
       
        public ItemsController(IItemsService itemService )
        {
            service = itemService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            return View(data);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create()
        {
            var dropdowm = await service.NewItemDropDownAsync();
            ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(Items items)
        { 
            try
            {
                await service.CreateItemAsync(items);


            }
            catch(Exception ex)
            {
               ModelState.AddModelError("","Error Adding Item"+ ex.Message);
                var dropdowm = await service.NewItemDropDownAsync();
                ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
                return View(items);

            }
            return RedirectToAction("Index","Home");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var data = await service.GetItemByIdAsync(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            var response = new EditItemVm()
            {
                Id = data.Id,
                Description = data.Description,
                Imageurl = data.Imageurl,
                Name
                = data.Name,
                CategoryId = data.CategoryId,
                Price = data.Price,
            };
            var drop = await service.NewItemDropDownAsync();
            ViewBag.Category = new SelectList(drop.categories, "Id", "Name");
            return View(response);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id,EditItemVm items)
        {
            if (id != items.Id)
            {
                return View("NOT FOUND");
            }
            if (!ModelState.IsValid)
            {
                var dropdowm = await service.NewItemDropDownAsync();
                ViewBag.Category = new SelectList(dropdowm.categories, "Id", "Name");
                //return View(items);
            }
            await service.UpdateItemAsync(items);
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var data = await service.GetItemByIdAsync(id);
            if (data == null) {
                return View("NOT FOUND");
            }
            return View(data);
        }
        [HttpPost , ActionName("Delete")]

        public async Task<IActionResult> Deleteitem(int id)
        {
           var data = await service.GetItemByIdAsync(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            await service.DeleteById(id);
            return RedirectToAction("Index","Home");
        }
    }

}
