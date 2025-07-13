using Mat3am_Elhabaib.DataBase.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAllItemsPartial()
        {
            var items = await _itemsService.GetAll(); // أو أي طريقة تجيب كل الـ Items
            return PartialView("_ItemsPartial", items);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchTarget)
        {
            var AllItems = await _itemsService.GetAll();
            var SearchResult = AllItems.Where(i => i.Name.Contains(searchTarget, StringComparison.OrdinalIgnoreCase) ||
            i.Description.Contains(searchTarget, StringComparison.OrdinalIgnoreCase)).ToList();
            // search an item with name or description
            if (string.IsNullOrWhiteSpace(searchTarget))
            {
                return PartialView("_ItemsPartial", AllItems);

            }
            else
            {
                return PartialView("_ItemsPartial", SearchResult);
            }
        }
    }
}
