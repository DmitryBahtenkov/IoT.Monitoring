using Iot.Main.Domain.Models.UserModel.DTO;

namespace Iot.Main.Domain.Shared.Defaults;

public static class DefaultUsers
{
    public static CreateUserRequest CreateUser(string companyName, int roleId) => new()
    {
        FirstName = $"{companyName}-admin",
        LastName = $"{companyName}-admin",
        Login = $"{companyName}@iot.default",
        Password = $"{companyName}-admin",
        ConfrmPassword = $"{companyName}-admin",
        RoleId = roleId
    };
}
