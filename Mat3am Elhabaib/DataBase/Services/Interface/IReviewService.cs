using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Interface;
using Mat3am_Elhabaib.Models;

namespace Mat3am_Elhabaib.DataBase.Services.Interface
{
    public interface IReviewService : IEntityBaserepo<Review> 
    {
        Task<IEnumerable<Review>> GetReviewByUserIdAndRoleAsync(string userId,string userrole);
        Task AddReviewAsync(Review review);
        Task UpadateReviewAsync(EditReviewVm reviewVm); 
    }
}
