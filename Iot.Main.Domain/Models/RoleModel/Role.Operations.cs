using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Domain.Models.RoleModel;

public partial class Role
{
    public static Role Create(CreateRoleRequest request, Company company)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Name, company);

        return new Role(request.Name, company);
    }

    private static void Deduplicate(string name, Company company)
    {
        if(company.Roles.Any(x => x.Name == name))
        {
            throw new BusinessException("Такая роль уже существует");
        }
    }

    private void Deduplicate(string name)
    {
        Deduplicate(name, Company);
    }

    public void Update(UpdateRoleRequest request)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Name);

        Name = request.Name;
    }
}
