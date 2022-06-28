using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.UserModel.DTO;

public record ChangePasswordRequest
{
    [Required(ErrorMessage = "Введите пароль")]
    public string OldPassword { get; set; }
    [Required(ErrorMessage = "Введите новый пароль")]
    public string NewPassword { get; set; }
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare(nameof(NewPassword), ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}
