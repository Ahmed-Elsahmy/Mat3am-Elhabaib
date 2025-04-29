using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Mat3am_Elhabaib.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public Items ShoppingCartItems { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
