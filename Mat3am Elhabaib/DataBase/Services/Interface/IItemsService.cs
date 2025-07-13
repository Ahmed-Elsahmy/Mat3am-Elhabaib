using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface IItemsService:IEntityBaserepo<Items>
    {
        Task<Items> GetItemByIdAsync(int id);

        Task CreateItemAsync(Items item, IFormFileCollection images);

        Task<bool> UpdateItemAsync(EditItemVm item , IFormFileCollection images);
        Task<NewItemDropDown> NewItemDropDownAsync();
        Task<IEnumerable<Items>> GetItemsByCategoryIdAsync(int categoryId);

    }
}
