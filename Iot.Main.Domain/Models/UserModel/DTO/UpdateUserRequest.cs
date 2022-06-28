using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.UserModel.DTO;

public record UpdateUserRequest
{
    [Required(ErrorMessage = "Введите имя")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Введите фамилию")]
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [Required(ErrorMessage = "Укажите роль")]
    public int RoleId { get; set; }
}
