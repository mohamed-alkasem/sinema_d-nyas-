using Sinema_Dunyasi.Data;
using Sinema_Dunyasi.Models.Domain;
using Sinema_Dunyasi.Models.DTO;
using Sinema_Dunyasi.Repositories.Abstract;

// ------------- Server side paging 14 -------------------
namespace Sinema_Dunyasi.Repositories.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext _databaseContext;
        public MovieService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //-----------------MovieService Add--------------------------------------------------//
        public bool Add(Movie model)
        {
            try
            {

                _databaseContext.Movie.Add(model);
                _databaseContext.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = model.Id,
                        GenreId = genreId
                    };
                    _databaseContext.MovieGenre.Add(movieGenre);
                }
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------MovieService Delete--------------------------------------------------//
        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var movieGenres = _databaseContext.MovieGenre.Where(a => a.MovieId == data.Id);
                foreach (var movieGenre in movieGenres)
                {
                    _databaseContext.MovieGenre.Remove(movieGenre);
                }
                _databaseContext.Movie.Remove(data);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------MovieService GetById--------------------------------------------------//

        public Movie GetById(int id)
        {
            return _databaseContext.Movie.Find(id);
        }

        //-----------------MovieService List--------------------------------------------------//

        public MovieListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new MovieListVm();

            var list = _databaseContext.Movie.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }
            // server side paging isteği no 14 --------------
            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var movie in list)
            {
                var genres = (from genre in _databaseContext.Genre
                              join mg in _databaseContext.MovieGenre
                              on genre.Id equals mg.GenreId
                              where mg.MovieId == movie.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                movie.GenreNames = genreNames;
            }
            data.MovieList = list.AsQueryable();
            return data;
        }

        //-----------------MovieService Update--------------------------------------------------//

        public bool Update(Movie model)
        {
            try
            {
                // these genreIds are not selected by users and still present is movieGenre table corresponding to
                // this movieId. So these ids should be removed.
                var genresToDeleted = _databaseContext.MovieGenre.Where(a => a.MovieId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach (var mGenre in genresToDeleted)
                {
                    _databaseContext.MovieGenre.Remove(mGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var movieGenre = _databaseContext.MovieGenre.FirstOrDefault(a => a.MovieId == model.Id && a.GenreId == genId);
                    if (movieGenre == null)
                    {
                        movieGenre = new MovieGenre { GenreId = genId, MovieId = model.Id };
                        _databaseContext.MovieGenre.Add(movieGenre);
                    }
                }

                _databaseContext.Movie.Update(model);
                // we have to add these genre ids in movieGenre table
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------MovieService GetGenreByMovieId--------------------------------------------------//

        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = _databaseContext.MovieGenre.Where(a => a.MovieId == movieId).Select(a => a.GenreId).ToList();
            return genreIds;
        }

        //------------------------------------------------------------------------------------------------//
    }
}
