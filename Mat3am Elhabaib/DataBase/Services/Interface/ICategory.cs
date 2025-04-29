using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface ICategory : IEntityBaserepo<Category>  
    {
        Task<Category> GetCategorybyIdAsync(int id);
    }
}