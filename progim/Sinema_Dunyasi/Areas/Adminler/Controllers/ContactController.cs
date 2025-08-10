using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Data;

namespace Sinema_Dunyasi.Areas.Adminler.Controllers
{
    [Area("Adminler")]
    public class ContactController : Controller
    {
        
        private readonly DatabaseContext _databaseContext;

        public ContactController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //-----------------Contact ContactList--------------------------------------------------//
        public IActionResult ContactList()
        {
            var contacts = _databaseContext.Contacts.OrderByDescending(c => c.SubmittedAt).ToList();
            return View(contacts);
        }

        //--------------------------------------------------------------------------------------//
    }
}
