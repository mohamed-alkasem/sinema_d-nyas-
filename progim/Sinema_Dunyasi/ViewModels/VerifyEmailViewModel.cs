using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}