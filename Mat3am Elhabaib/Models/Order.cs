using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class Order: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string  UserId { get; set; }
        [ForeignKey("UserId")]
        public  User user { get; set; }
        public List<OrderItems> OrderItems { get; set; }
        public bool Delevird { get; set; } = false;
    }
}
