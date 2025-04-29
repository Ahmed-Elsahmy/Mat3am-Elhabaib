using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Repo.Impelementation;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mat3am_Elhabaib.DataBase.Services.Impelementation
{
    public class ReviewService : EntityBaseRepo<Review>, IReviewService
    {
        private readonly AppDbContext _context;
        public ReviewService(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddReviewAsync(Review review)
        {
            var data = new Review()
            {
                UserId = review.UserId,
                Rate = review.Rate,
                Comment = review.Comment,
            };
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByUserIdAndRoleAsync(string userId, string userrole)
        {
            var data = await _context.reviews.Include(u=>u.user).ToListAsync();
            if (userrole != "Admin")
            {
                data = data.Where(u=>u.UserId==userId).ToList();
            }
            return data;
        }

        public async Task UpadateReviewAsync(EditReviewVm reviewVm)
        {
            var data = await _context.reviews.FirstOrDefaultAsync(i => i.Id==reviewVm.Id);
            if (data != null) { 
                data.Rate= reviewVm.Rate;
                data.Comment=reviewVm.Comment;
                await _context.SaveChangesAsync();
            }
        }
    }
}
