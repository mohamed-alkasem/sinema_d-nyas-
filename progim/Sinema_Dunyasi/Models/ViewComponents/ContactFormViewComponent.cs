using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Models.Domain;

namespace Sinema_Dunyasi.Models.ViewComponents
{
    public class ContactFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new Contact());
        }
    }
}
