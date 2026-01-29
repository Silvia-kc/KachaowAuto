using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels
{
    public class RegisterViewModel 
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
