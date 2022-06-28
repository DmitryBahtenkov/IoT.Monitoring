using Iot.Main.Domain.Models.RoleModel.DTO;

namespace Iot.Main.Domain.Shared.Defaults;

public static class DefaultRoles
{
    public static CreateRoleRequest AdminRole(string companyName) => new CreateRoleRequest
    {
        Name = $"Администратор ({companyName})"
    };
}
