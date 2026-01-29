using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels
{
    public class LoginViewModel 
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
