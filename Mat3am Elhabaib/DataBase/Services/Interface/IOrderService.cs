using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;
using System.Drawing;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface IOrderService:IEntityBaserepo<Order>
    {
        Task StoreOrderAsync(List<ShoppingCart> shoppingCarts , string UserId);
        Task<List<Order>> GetOrderByUserIDAndRoleAsync(string UserId, string UserRole);
        Task<bool> DeleteOrderAsync(int OrderID);

        Task<Order > GetOrderByIdAsync(int OrderID);
    }
}
