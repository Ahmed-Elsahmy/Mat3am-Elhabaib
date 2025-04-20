using Mat3am_Elhabaib.Models;

namespace Mat3am_Elhabaib.DataBase.ModelVM
{
    public class NewItemDropDown
    {
        public NewItemDropDown() { 
            categories = new List<Category>();
        }
        public List<Category> categories { get; set; }
    }
}
