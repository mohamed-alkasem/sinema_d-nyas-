﻿using System.ComponentModel.DataAnnotations;


using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email gereklidir.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "{0} en az {2} ve en fazla {1} karakter uzunluğunda olmalıdır.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre onayı gereklidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifreyi Onayla")]
        public string ConfirmNewPassword { get; set; }
    }
}
