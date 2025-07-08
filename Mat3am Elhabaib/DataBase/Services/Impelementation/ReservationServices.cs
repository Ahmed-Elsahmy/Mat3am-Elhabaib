using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;

namespace Mat3am_Elhabaib.DataBase.Services.Impelementation
{
    public class ReservationServices : EntityBaseRepo<Reservations>, IReservationServices
    {
        private readonly AppDbContext _appDbContext;
        public ReservationServices(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateReservationAsync(Reservations reservations)
        {
           
            var data = new Reservations()
            {
                UserID = reservations.UserID,
                NumberOftables = reservations.NumberOftables,
                PhoneNumber = reservations.PhoneNumber,
                time = reservations.time,

            };
            await _appDbContext.reservations.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservations>> GetReservationsByUserIDAndRoleAsync(string userID, string roleID)
        {
            var data = await _appDbContext.reservations.Include(u => u.user).ToListAsync();
            if(roleID != "Admin")
            {
                data = data.Where(u=>u.UserID == userID).ToList();
            }
            return data;
        }

        public async Task UpdateReservationAsync(EditResevationVm editResevation)
        {
            var data = await _appDbContext.reservations.FirstOrDefaultAsync(i => i.Id == editResevation.Id);
            if (data != null) {
                data.time = editResevation.time;
                data.NumberOftables = editResevation.NumberOftables;
                data.PhoneNumber = editResevation.PhoneNumber;
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
