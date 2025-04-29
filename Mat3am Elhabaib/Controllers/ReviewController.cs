using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Migrations;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mat3am_Elhabaib.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index() { 
        
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userrole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _reviewService.GetReviewByUserIdAndRoleAsync(userid, userrole);
            return View(orders);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new Review());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Review review)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
            {
                // Handle case where UserId is missing or user is not authenticated
                ModelState.AddModelError("", "User is not authenticated.");
                return View(review);
            }
            review.UserId = user;
            try
            {
                await _reviewService.AddReviewAsync(review);

            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", "Error Create Review" + ex.Message);
                return View(review);

            }
            return RedirectToAction("Index","Review");
        }
        //Get/Edit/1
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _reviewService.GetbyId(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            var response = new EditReviewVm()
            {
                Id = data.Id,
                UserId = data.UserId,
                Rate = data.Rate,
                Comment = data.Comment,

            };
            return View(response);
        }
        //Post/Edit/1/review
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id , EditReviewVm review )
        {
            if (id != review.Id)
            {
                return View("NOT FOUND");
            }
            try
            {
                await _reviewService.UpadateReviewAsync(review);

            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Error Editing Review" + ex.Message);
                return View(review);
            }

            return RedirectToAction("Index", "Review");
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
             var data = await _reviewService.GetbyId(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            return View(data);
        }
        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteConf(int id)
        {
            var data = await _reviewService.GetbyId(id);
            if (data == null)
            {
                return View("NOT FOUND");
            }
            await _reviewService.DeleteById(id);
            return RedirectToAction("Index", "Review");
        }
    }
}
