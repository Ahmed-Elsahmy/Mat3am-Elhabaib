using Mat3am_Elhabaib.DataBase.Repo.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mat3am_Elhabaib.Models
{
    public class Category:IEntityBase
    {
        [Key]
        public  int Id { get; set; }
        [Required(ErrorMessage ="Please add a name for a category")]
        public string Name { get; set; }
        public List<Items> items { get; set; }
    }
}
