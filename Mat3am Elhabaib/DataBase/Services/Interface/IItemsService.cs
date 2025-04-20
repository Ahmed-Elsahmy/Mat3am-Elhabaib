using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface IItemsService:IEntityBaserepo<Items>
    {
        Task<Items> GetItemByIdAsync(int id);

        Task CreateItemAsync(Items item);

        Task UpdateItemAsync(Items item);
        Task<NewItemDropDown> NewItemDropDownAsync();
    }
}
