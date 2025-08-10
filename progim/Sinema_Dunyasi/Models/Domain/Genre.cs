using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.Models.Domain 
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen film türünü gereklidir .")]
        public string? GenreName { get; set; }
    }
}
