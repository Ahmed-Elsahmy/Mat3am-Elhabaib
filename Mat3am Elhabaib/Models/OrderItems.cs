using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class OrderItems: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Items  items { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order  order { get; set; }
    }
}
