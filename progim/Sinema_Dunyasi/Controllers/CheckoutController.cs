using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sinema_Dunyasi.Controllers
{
    public class CheckoutController : Controller
    {
        //-----------------Checkout Index--------------------------------------------------//

        public IActionResult Index()
        {
            return View();
        }

        //-----------------Checkout ProcessPayment--------------------------------------------------//

        [HttpPost]
        public IActionResult ProcessPayment(string fullName, string cardNumber, string expiryDate, string cvv)
        {
            return RedirectToAction("PaymentSuccess");
        }

        //-----------------Checkout PaymentSuccess--------------------------------------------------//
  
        public IActionResult PaymentSuccess()
        {
            return View();
        }


        //------------------------------------------------------------------------------------------//

    }
}
