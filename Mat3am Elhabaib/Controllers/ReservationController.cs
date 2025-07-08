using Mat3am_Elhabaib.DataBase.ModelVM;
using Mat3am_Elhabaib.DataBase.Services.Interface;
using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mat3am_Elhabaib.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationServices _reservationServices;
        public ReservationController(IReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userrole = User.FindFirstValue(ClaimTypes.Role);
            var data = await _reservationServices.GetReservationsByUserIDAndRoleAsync(userId, userrole);
            return View(data);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View(new Reservations());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Reservations reservations)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
            {
                ModelState.AddModelError("", "User Is Not Authenticated");
                return View(reservations);
            }
        
            reservations.UserID = user;
            //if (reservations.PhoneNumber == null)
            //{
            //    reservations.PhoneNumber = reservations.user.PhoneNumber;
            //}
            if (reservations.NumberOftables <= 0)
            {
                ModelState.AddModelError("", "Number Of Table Can't be zero to continue reservation .. !");
                return View(reservations);
            }
            if(reservations.time < DateTime.Now)
            {
                ModelState.AddModelError("", "Reservation date Can't be in Past Date .. !");
                return View(reservations);
            }

            try
            {
                await _reservationServices.CreateReservationAsync(reservations);
            }

            catch (Exception ex) {
                ModelState.AddModelError("", "Error Carete A Reservation");
                return View(reservations);
            }
            return RedirectToAction("Index", "Reservation");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _reservationServices.GetbyId(id);
            if (data == null) {
                return View("Not Found");
            }
            var response = new EditResevationVm()
            {
                Id = data.Id,
                NumberOftables = data.NumberOftables,
                PhoneNumber = data.PhoneNumber,
                time = data.time
            };
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditResevationVm model)
        {
            if (id != model.Id)
            {
                return View("Not Found");
            }
            if (model.time < DateTime.Now)
            {
                ModelState.AddModelError("", "Reservation date Can't be in Past Date .. !");
                return View(model);
            }
            if (model.NumberOftables <= 0) {
                ModelState.AddModelError("", "Number Of Table Can't be zero to continue reservation .. ! ");
                return View(model);
            }
            try
            {
                await _reservationServices.UpdateReservationAsync(model);
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Error Editing The Reservation");
                return View(model);
            }
            return RedirectToAction("Index", "Reservation");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _reservationServices.GetbyId(id);
            if (data == null) {
                return View("Not Found");

            }
            return View(data);
        }
        [HttpPost , ActionName("Delete")]
        public async  Task<IActionResult> DeleteConf(int id)
        {

            var data = await _reservationServices.GetbyId(id);
            if (data == null)
            {
                return View("Not Found");

            }
            await _reservationServices.DeleteById(id);
            return RedirectToAction("Index", "Reservation");
        }
    } }
