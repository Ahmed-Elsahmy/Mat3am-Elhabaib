using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mat3am_Elhabaib.DataBase.Repo.Interface;

namespace Mat3am_Elhabaib.Models
{
    public class Invoice: IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public double? TotallPrice { get; set; }
        public double? NetPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool? IsPaid { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User user { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? order { get; set; }
    }
}
