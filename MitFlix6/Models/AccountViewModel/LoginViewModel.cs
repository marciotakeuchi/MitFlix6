//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace MitFlix6.Models.AccountViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Lembrar login")]
        public bool Rememberme { get; set; }
    }
}
