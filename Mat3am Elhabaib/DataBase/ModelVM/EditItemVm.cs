using Mat3am_Elhabaib.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class EditItemVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please add a name for an item")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please add a description for an item")]
        public string Description { get; set; }

        public List<string> Images { get; set; }

        [Required(ErrorMessage = "Please add a price for an item")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        //[ForeignKey(nameof(CategoryId))]
        //public Category category { get; set; }
    }
}
