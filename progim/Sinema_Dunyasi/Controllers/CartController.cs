using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Data;
using Sinema_Dunyasi.Helpers;
using Sinema_Dunyasi.Models.Domain;

namespace Sinema_Dunyasi.Controllers
{
    public class CartController : Controller
    {

        private readonly DatabaseContext _databaseContext;
        public CartController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //-----------------Cart Index--------------------------------------------------//

        public IActionResult Index()
        {
            // Session'dan sepeti al
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Toplam tutarı hesapla
            int? total = cart.Any() ? cart.Sum(c => c.Movie.Price * c.Quantity) : 0;

            ViewBag.Total = total; // Toplam tutarı ViewBag ile gönder
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View(cart);
        }

        //-----------------Cart AddToCart--------------------------------------------------//

        public IActionResult AddToCart(int movieId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Veritabanından ürünü getir
            var movie = _databaseContext.Movie.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
                return NotFound();

            // Sepette bu üründen varsa miktarı artır
            var existingItem = cart.FirstOrDefault(c => c.Movie.Id == movieId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Movie = movie,
                    Quantity = 1
                });
            }

            // Güncellenen sepeti Session'a kaydet
            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToAction("Index");
        }

        //-----------------Cart RemoveFromCart--------------------------------------------------//

        public IActionResult RemoveFromCart(int movieId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Ürünü sepette bul ve kaldır
            var itemToRemove = cart.FirstOrDefault(c => c.Movie.Id == movieId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }

            HttpContext.Session.SetObject("Cart", cart);

          //  TempData["SuccessMessage"] = "Ürün sepetinizden başarıyla kaldırıldı."; // Sepetten ürün kaldırıldı mesajı

            return RedirectToAction("Index");
        }

        //-----------------Cart ClearCart--------------------------------------------------//
       
        public IActionResult ClearCart()
        {
            // Sepeti temizle
            HttpContext.Session.Remove("Cart");

            // Kullanıcıyı ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }


        //--------------------------------------------------------------------------------//
    }
}
