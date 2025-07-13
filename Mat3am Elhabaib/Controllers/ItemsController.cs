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

        public async Task<IActionResult> Create(Items items, IFormFileCollection images)
        { 
            try
            {
                Console.WriteLine("Images received: " + images.Count);
                await service.CreateItemAsync(items,images);


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
                Images = data.Images,
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
        public async Task<IActionResult> Edit(EditItemVm item, IFormFileCollection images)
        {

            var data = await service.GetItemByIdAsync(item.Id);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}")
    .ToList();
                if (images == null || images.Count == 0)
                {
                    item.Images = data.Images; // رجع الصور القديمة
                }

                var drop = await service.NewItemDropDownAsync();
                ViewBag.Category = new SelectList(drop.categories, "Id", "Name");
                return View(item);
            }

            var result = await service.UpdateItemAsync(item, images);
            if (!result)
                return View("NOT FOUND");

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
            foreach (var oldImage in data.Images)
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgItem/Profile", oldImage);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            data.Images.Clear();
            await service.DeleteById(id);
            return RedirectToAction("Index","Home");
        }
    }

}
