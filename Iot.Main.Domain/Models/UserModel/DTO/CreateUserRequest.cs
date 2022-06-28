using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.UserModel.DTO;

public record CreateUserRequest
{
    [Required(ErrorMessage = "Введите имя")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Введите фамилию")]
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [Required(ErrorMessage = "Введите логин"), EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfrmPassword { get; set; }
    [Required(ErrorMessage = "Укажите роль")]
    public int RoleId { get; set; }
}
