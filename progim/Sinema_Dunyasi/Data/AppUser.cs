using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sinema_Dunyasi.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Email { get; internal set; }
    }
}
