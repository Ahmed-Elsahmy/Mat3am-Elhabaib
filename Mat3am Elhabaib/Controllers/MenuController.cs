using Mat3am_Elhabaib.DataBase.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Mat3am_Elhabaib.Controllers
{
    public class MenuController : Controller
    {
        private readonly ICategory _categoryService;
        private readonly IItemsService _itemsService;

        public MenuController(ICategory categoryService, IItemsService itemsService)
        {
            _categoryService = categoryService;
            _itemsService = itemsService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            var items = await _itemsService.GetAll();
            ViewBag.Categories = categories;
            return View(items);
        }

        public async Task<IActionResult> FilterItems(int categoryId)
        {
            var filteredItems = await _itemsService.GetItemsByCategoryIdAsync(categoryId);
            return PartialView("_ItemsPartial", filteredItems);
        }

    }
}
