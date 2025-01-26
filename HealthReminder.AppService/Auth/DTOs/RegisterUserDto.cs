using System.ComponentModel.DataAnnotations;

namespace HealthReminder.AppService.Auth.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmação de senha é obrigatória.")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}