using Sinema_Dunyasi.Data;
using Sinema_Dunyasi.Models.Domain;
using Sinema_Dunyasi.Repositories.Abstract;

namespace Sinema_Dunyasi.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext _databaseContext;
        public GenreService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        //-----------------GenreService Add--------------------------------------------------//

        public bool Add(Genre model)
        {
            try
            {
                _databaseContext.Genre.Add(model);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------GenreService Delete--------------------------------------------------//

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                _databaseContext.Genre.Remove(data);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------GenreService GetById--------------------------------------------------//

        public Genre GetById(int id)
        {
            return _databaseContext.Genre.Find(id);
        }

        //-----------------GenreService List--------------------------------------------------//

        public IQueryable<Genre> List()
        {
            var data = _databaseContext.Genre.AsQueryable();
            return data;
        }

        //-----------------GenreService Update--------------------------------------------------//

        public bool Update(Genre model)
        {
            try
            {
                _databaseContext.Genre.Update(model);
                _databaseContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-------------------------------------------------------------------------------------//

    }
}
