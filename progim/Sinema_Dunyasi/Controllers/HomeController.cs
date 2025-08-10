using Microsoft.AspNetCore.Mvc;
using Sinema_Dunyasi.Models;
using Sinema_Dunyasi.Repositories.Abstract;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace Sinema_Dunyasi.Controllers
{
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

        //-----------------Home About--------------------------------------------------//

        public IActionResult Most_Viewed()
        {
            return View();
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
