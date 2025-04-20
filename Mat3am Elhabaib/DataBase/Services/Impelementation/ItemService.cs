using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase.Services.Impelementation
{
    public class ItemService : EntityBaseRepo<Items>, IItemsService
    {
        private AppDbContext _context;

        public ItemService(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CreateItemAsync(Items item)
        {
            var Newitem = new Items()
            {
                Id = item.Id,
                Name = item.Name,
                Imageurl
                = item.Imageurl,
                CategoryId = item.CategoryId,
                Description = item.Description,
                Price = item.Price,
                Isavailable = item.Isavailable,
            };
            await _context.items.AddAsync(Newitem);
            await _context.SaveChangesAsync();
        }

        public async Task<Items> GetItemByIdAsync(int id)
        {
            var data = await _context.items.Include(cat => cat.category).FirstOrDefaultAsync(i => i.Id == id);
            return data;
        }

        public async Task<NewItemDropDown> NewItemDropDownAsync()
        {
            var DropDown = new NewItemDropDown()
            {
                categories = await _context.categories.OrderBy(i => i.Id).ToListAsync()
            };
            return DropDown;
        }

        public async Task UpdateItemAsync(Items item)
        {
            var data = await _context.items.FirstOrDefaultAsync(i =>i.Id == item.Id);
            if (data != null) { 
                data.Name = item.Name;
                data.Imageurl = item.Imageurl;
                data.CategoryId = item.CategoryId;
                data.Description = item.Description;
                data.Price = item.Price;
                data.Isavailable = item.Isavailable;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
