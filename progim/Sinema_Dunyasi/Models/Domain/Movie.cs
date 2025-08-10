using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Sinema_Dunyasi.Models.Domain
{

    // ------ Server side validation 11 --------------
    public class Movie
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Başlık gereklidir .")]
        public string? Title { get; set; }

        public string? ReleaseYear { get; set; }

        public string? MovieImage { get; set; }

        [Required(ErrorMessage = "Lütfen kadro gereklidir .")]
        public string? Cast { get; set; }

        [Required(ErrorMessage = "Lütfen film yönetmeni gereklidir .")]
        public string? Director { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Lütfen filmin türü seçmelidir.")]
        public List<int>? Genres { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }

        [NotMapped]
        public string? GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }


        [Required(ErrorMessage = "Lütfen film fiyati gereklidir .")]
        [DisplayName("Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public int? Price { get; set; }


    }
}
