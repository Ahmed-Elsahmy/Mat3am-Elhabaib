using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class Items:IEntityBase
    {
        [Key]
        public  int Id { get; set; }
        [Required(ErrorMessage ="Please add a name for an item")]
        public  string Name  { get; set; }
        [Required(ErrorMessage ="Please add a description for an item")]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string  Imageurl { get; set; }
        [Required(ErrorMessage ="Please add a price for an item")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category category { get; set; }

    }
}
