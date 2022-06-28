using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iot.Main.Domain.Models.RoleModel.DTO;

public class CreateRoleRequest
{
    [Required(ErrorMessage = "Введите название роли")]
    public string Name { get; set; }
}
