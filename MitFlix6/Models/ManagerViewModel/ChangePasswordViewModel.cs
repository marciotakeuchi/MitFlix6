using System.ComponentModel.DataAnnotations;

namespace MitFlix6.Models.ManagerViewModel
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Senha atual")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [StringLength(100, ErrorMessage ="O campo {0} deve ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage ="As senhas devem ser iguais")]
        public string ConfirmPassword { get; set; }
        public string? StatusMessage { get; set; }
    }
}
