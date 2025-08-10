using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.ViewModels
{
    public class AdminLoginModel
    {
        [Required(ErrorMessage = "TC No gereklidir!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
