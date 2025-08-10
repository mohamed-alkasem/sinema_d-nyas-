using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Models.Domain;
using Sinema_Dunyasi.Repositories.Abstract;

namespace Sinema_Dunyasi.Areas.Adminler.Controllers
{
    [Area("Adminler")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //-----------------Genre Add--------------------------------------------------//

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Başarıyla Eklendi";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Sunucu tarafında hata";
                return View(model);
            }
        }


        //-----------------Genre Edit  Update  --------------------------------------------------//

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Başarıyla Eklendi";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Sunucu tarafında hata";
                return View(model);
            }
        }


        //-----------------Genre GenreList--------------------------------------------------//
        public IActionResult GenreList()
        {
            var data = this._genreService.List().ToList();
            return View(data);
        }


        //-----------------Genre Delete--------------------------------------------------//
        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }

        //-------------------------------------------------------------------------------//
    }
}
