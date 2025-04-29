using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;
using System.Collections;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface IReservationServices : IEntityBaserepo<Reservations> 
    {
        Task<IEnumerable<Reservations>> GetReservationsByUserIDAndRoleAsync(string userID, string roleID);
        Task CreateReservationAsync(Reservations reservations);
        Task UpdateReservationAsync(EditResevationVm editResevation);
    }
}
