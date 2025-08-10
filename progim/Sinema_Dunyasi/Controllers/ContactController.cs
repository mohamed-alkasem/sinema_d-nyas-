using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Data;
using Sinema_Dunyasi.Models.Domain;
using System.Linq;

namespace Sinema_Dunyasi.Controllers
{
    public class ContactController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public ContactController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //-----------------Checkout ContactForm-----------İletişim formunu göster---------------------------------------//
        
        public IActionResult ContactForm()
        {
            return View();
        }

        //-----------------Checkout ContactForm-----------Formdan gelen veriyi kaydet---------------------------------------//

        [HttpPost]
        public IActionResult ContactForm(Contact model)
        {
            // Kullanıcı oturum açmış mı kontrol et
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Mesaj göndermek için giriş yapmanız gerekmektedir.";
                return RedirectToAction("Login", "Account"); // Kullanıcıyı giriş sayfasına yönlendir
            }

            // Model doğrulama ve veri kaydetme
            if (ModelState.IsValid)
            {
                model.SubmittedAt = DateTime.Now; // Mesaj gönderim zamanını ekleyin
                _databaseContext.Contacts.Add(model);
                _databaseContext.SaveChanges();
                TempData["Success"] = "Mesajınız başarıyla gönderildi!";
                return RedirectToAction("ContactForm");
            }

            return View(model);
        }


        //------------------------------------------------------------------------------------------------------------------//
    }
}
