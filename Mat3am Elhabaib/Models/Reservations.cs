using Mat3am_Elhabaib.DataBase;
using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class Reservations : IEntityBase
    {
        [Key]
        public  int Id { get; set; }
        [Required]
        public int NumberOftables { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime time { get; set; } = DateTime.Now;

        public string? PhoneNumber { get; set; }
        public string UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User user { get; set; }
      }
}
