using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MitFlix6.Models.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Usuario")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Senha")]
        [StringLength(100,ErrorMessage ="O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres",MinimumLength =8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirmar senha")]
        [Compare("Password", ErrorMessage ="As senhas devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
} 
