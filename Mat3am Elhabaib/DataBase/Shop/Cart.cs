using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase.Shop
{
    public class Cart
    {
        public AppDbContext _context { get; set; }
        public string ShopingCartId { get; set; }
        public List<ShoppingCart> ShopingItems { get; set; }
        public Cart(AppDbContext context)
        {
            _context = context;
        }

        public static Cart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new Cart(context) { ShopingCartId = cartId };
        }

        public void AddItemToCart(Items items)
        {
            var shoppingCartItem = _context.shoppingCarts.FirstOrDefault(n => n.ShoppingCartItems.Id == items.Id && n.ShoppingCartId == ShopingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCart()
                {
                    ShoppingCartId = ShopingCartId,
                    ShoppingCartItems = items,
                    Amount = 1
                };

                _context.shoppingCarts.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Items items)
        {
            var shoppingCartItem = _context.shoppingCarts.FirstOrDefault(n => n.ShoppingCartItems.Id == items.Id && n.ShoppingCartId == ShopingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.shoppingCarts.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCart> GetShoppingCartItems()
        {
            return ShopingItems ?? (ShopingItems = _context.shoppingCarts.Where(n => n.ShoppingCartId == ShopingCartId).Include(n => n.ShoppingCartItems).ToList());
        }

        public double GetShoppingCartTotal() => (double)_context.shoppingCarts.Where(n => n.ShoppingCartId == ShopingCartId).Select(n => n.ShoppingCartItems.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.shoppingCarts.Where(n => n.ShoppingCartId == ShopingCartId).ToListAsync();
            _context.shoppingCarts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
