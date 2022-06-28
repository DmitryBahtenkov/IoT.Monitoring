using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.RoleModel.DTO;

public class UpdateRoleRequest
{
    [Required(ErrorMessage = "Введите название роли")]
    public string Name { get; set; }
}
