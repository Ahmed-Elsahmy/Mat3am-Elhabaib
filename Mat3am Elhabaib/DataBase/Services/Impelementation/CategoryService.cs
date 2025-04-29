using Mat3am_Elhabaib.DataBase.Repo.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase.Services.Impelementation
{
    public class CategoryService : EntityBaseRepo<Category>, ICategory
    {
        private readonly AppDbContext _appDbContext;
        public CategoryService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Category> GetCategorybyIdAsync(int id)
        {
            var data = await _appDbContext.categories.Include(n => n.items).FirstOrDefaultAsync(n => n.Id == id);
            return data;
        }
    }
}
