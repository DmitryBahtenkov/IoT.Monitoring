using System.ComponentModel.DataAnnotations;
using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Models.UserModel.DTO;

namespace Iot.Main.Domain.Aggregates;

public class RegisterAggregate
{
    [Required(ErrorMessage = "Введите имя")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Введите фамилию")]
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    [Required(ErrorMessage = "Введите логин"), EmailAddress(ErrorMessage = "Неверный формат логина")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    public string ConfrmPassword { get; set; }
    [Required(ErrorMessage = "Введите название компании")]
    public string CompanyName { get; set; }
    [Required(ErrorMessage = "Введите описание компании")]
    public string CompanyDescription { get; set; }

    public CreateUserRequest ToUserRequest()
    {
        return new()
        {
            FirstName = FirstName,
            LastName = LastName,
            MiddleName = MiddleName,
            Login = Login,
            Password = Password,
            ConfrmPassword = ConfrmPassword
        };
    }

    public CreateCompanyRequest ToCompanyRequest()
    {
        return new()
        {
            Name = CompanyName,
            Description = CompanyDescription
        };
    }

    public LoginRequest ToLoginRequest()
    {
        return new()
        {
            Login = Login,
            Password = Password
        };
    }
}
