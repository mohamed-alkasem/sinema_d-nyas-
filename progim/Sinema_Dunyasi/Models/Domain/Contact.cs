using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.Models.Domain
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen adsoyad gereklidir .")]
        [StringLength(100)]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Lütfen e-posta gereklidir .")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Lütfen mesaj gereklidir .")]
        [StringLength(500)]
        public string Message { get; set; }


        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
