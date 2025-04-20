using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mat3am_Elhabaib.DataBase.Repo.Impelementation
{
    public class EntityBaseRepo<T> : IEntityBaserepo<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _appDbContext; // for more security
        public EntityBaseRepo(AppDbContext appDbContext) { 

            _appDbContext = appDbContext;
        }

        public async Task Add(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int Id)
        {
            var data = await _appDbContext.Set<T>().FirstOrDefaultAsync(id => id.id == Id);
            EntityEntry entity = _appDbContext.Entry<T>(data);
            entity.State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var data = await _appDbContext.Set<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetbyId(int id)
        {
            var data = await _appDbContext.Set<T>().FirstOrDefaultAsync(n => n.id == id);
            return data;
        }

        public async Task Update(int Id, T entity)
        {
            EntityEntry entityEntry = _appDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
