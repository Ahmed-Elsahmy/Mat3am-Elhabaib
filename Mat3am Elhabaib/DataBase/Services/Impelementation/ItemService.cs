using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.CodeAnalysis;
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

        public async Task CreateItemAsync(Items item, IFormFileCollection images)
        {
            var Newitem = new Items()
            {
                Name = item.Name,
                Images = new List<string>(),
                CategoryId = item.CategoryId,
                Description = item.Description,
                Price = item.Price,
            };

             // images folderpath
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgItem/Profile");
            //check already folder is exist 
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            // save all images
            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    // new imageextintion
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    //!full Path
                    var filePath = Path.Combine(folderPath, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    Newitem.Images.Add(uniqueFileName);
                }
            }

            // إضافة العنصر لقاعدة البيانات
            await _context.items.AddAsync(Newitem);
            await _context.SaveChangesAsync();
        }


        public async Task<Items> GetItemByIdAsync(int id)
        {
            var data = await _context.items.Include(cat => cat.category).FirstOrDefaultAsync(i => i.Id == id);
            return data;
        }

        public async Task<IEnumerable<Items>> GetItemsByCategoryIdAsync(int categoryId)
        {
            return await _context.items
                .Where(i => i.CategoryId == categoryId)
                .ToListAsync();
        }


        public async Task<NewItemDropDown> NewItemDropDownAsync()
        {
            var DropDown = new NewItemDropDown()
            {
                categories = await _context.categories.OrderBy(i => i.Id).ToListAsync()
            };
            return DropDown;
        }

        public async Task<bool> UpdateItemAsync(EditItemVm item, IFormFileCollection images)
        {
            var data = await _context.items.FirstOrDefaultAsync(i => i.Id == item.Id);
            if(data == null)
            {
                return false;
            }
            // delete old images
            if (data.Images != null && data.Images.Count > 0) {
                foreach (var image in data.Images) {
                    var oldimgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgItem/Profile", image);
                    if (System.IO.File.Exists(oldimgpath)) { 
                        System.IO.File.Delete(oldimgpath);
                    }
                }
            }
            //save new images
            var newimages = new List<string>();
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImgItem/Profile");
            //check folder is already exist elsecreate new
            if (!Directory.Exists(folderpath)) { 
                 Directory.CreateDirectory(folderpath);
            }
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    if (image != null && image.Length > 0)
                    {
                        var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(folderpath, uniqueName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        newimages.Add(uniqueName);
                    }
                }
            }
            data.Name = item.Name;
            data.Description = item.Description;
            data.Price = item.Price;
            data.CategoryId = item.CategoryId;
            data.Images = newimages;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
