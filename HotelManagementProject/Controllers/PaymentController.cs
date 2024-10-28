using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentServices)
        {
            _paymentService = paymentServices;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _paymentService.GetPaymentsAsync();
            return View(payments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Payment payments)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.CreatePaymentAsync(payments);
                return RedirectToAction("Index");
            }
            return View(payments);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PaymentMethod ,PaymentStatus ,PaymentDate ,PaymentAmount,ReservationId")] Payment paymentDetail)
        {
            if (id != paymentDetail.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _paymentService.UpdatePaymentAsync(id, paymentDetail);
                return RedirectToAction(nameof(Index));
            }

            return View(paymentDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
