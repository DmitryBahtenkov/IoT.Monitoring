using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.UserModel.DTO;

public record LoginRequest
{
    [Required(ErrorMessage = "Введите логин")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
}
