using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Repositories.Abstract;

namespace Sinema_Dunyasi.Areas.Adminler.Controllers
{
    [Area("Adminler")]
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //-----------------Home Index--------------------------------------------------//
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var movies = _movieService.List(term, true, currentPage);
            return View(movies);
        }


        //-----------------Home MovieDetail--------------------------------------------------//
        public IActionResult MovieDetail(int movieId)
        {
            var movie = _movieService.GetById(movieId);
            return View(movie);
        }


        //-----------------------------------------------------------------------------------//
    }
}
