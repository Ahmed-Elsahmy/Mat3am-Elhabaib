using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.DataBase.Shop;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mat3am_Elhabaib.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly Cart shoppingCart ;
        private readonly IOrderService orderService;

        public OrdersController(IItemsService _itemsService, Cart _shoppingCart, IOrderService _orderService )
        {
            itemsService = _itemsService;
            shoppingCart = _shoppingCart;
            orderService = _orderService;
        }
        //return all orders 
        public async Task<IActionResult> Index()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await orderService.GetOrderByUserIDAndRoleAsync(UserId, UserRole);
            return View(orders);
        }

        public  IActionResult ShopingCart()
        {
            var items =  shoppingCart.GetShoppingCartItems();
            shoppingCart.ShopingItems = items;

            var response = new ShoppingCartVm()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        [Authorize]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await itemsService.GetItemByIdAsync(id);

            if (item != null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShopingCart));
        }
        [Authorize]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await itemsService.GetItemByIdAsync(id);

            if (item != null)
            {
                shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShopingCart));
        }
        [Authorize]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            await orderService.StoreOrderAsync(items, userId);
            await shoppingCart.ClearShoppingCartAsync();
           
            return View("OrderCompleted");
        }
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound(); // Return a 404 if the order wasn't found
            }

            return RedirectToAction(nameof(Index)); // Redirect to the orders list
        }
    }
}
