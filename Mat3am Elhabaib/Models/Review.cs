using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class Review : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Range(0, 5)]
        [Required(ErrorMessage ="Please add a review rate")]
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public  string UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
    }
}

