using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase
{
    public class AppDbContext:IdentityDbContext<User> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext):base(dbContext) { }
        public DbSet<Items> items { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Reservations> reservations { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
    }
}
