using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase.Services.Impelementation
{
    public class OrderServices:IOrderService
    {
        private readonly AppDbContext appDbContext
            ;
        public OrderServices(AppDbContext _appDbContext)
        {
            appDbContext =_appDbContext ;
        }

        public async Task<bool> DeleteOrderAsync(int OrderID)
        {
            var order = await appDbContext.orders.Include(o => o.OrderItems)
                                             .FirstOrDefaultAsync(o => o.Id == OrderID);
            if (order == null)
            {
                return false;
            }

            // Remove the associated order items
            appDbContext.orderItems.RemoveRange(order.OrderItems);
            // Remove the order itself
            appDbContext.orders.Remove(order);

            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrderByUserIDAndRoleAsync(string UserId, string UserRole)
        {

            var orders = await  appDbContext.orders.Include(n => n.OrderItems).ThenInclude(n => n.items).Include(n => n.user).ToListAsync();

            if (UserRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == UserId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCart> items , string UserId)
        {
            var order = new Order()
            {
                UserId = UserId,
            };
            await appDbContext.orders.AddAsync(order);
            await appDbContext.SaveChangesAsync();

            foreach (var item in items)
            {
                var OrderItem = new OrderItems()
                {
                    Quantity = item.Amount,
                    ItemId = item.ShoppingCartItems.Id,
                    OrderId = order.Id,
                    UnitPrice = item.ShoppingCartItems.Price,
                    TotalPrice = item.ShoppingCartItems.Price * item.Amount
                };
                await appDbContext.orderItems.AddAsync(OrderItem);
            }
            await appDbContext.SaveChangesAsync();
        }
    }
}
