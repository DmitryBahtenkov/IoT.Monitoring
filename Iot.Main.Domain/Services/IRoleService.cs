#nullable enable
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.RoleModel.DTO;

namespace Iot.Main.Domain.Services;

public interface IRoleService
{
    public Task<Role> Create(CreateRoleRequest request);
    public Task<Role> Update(int id, UpdateRoleRequest request);
    public Task<Role?> Get(RoleFilter filter);
    public Task<List<Role>> GetAll(RoleFilter filter);
    public Task<Role> CreateForCompany(CreateRoleRequest request, Company company);
}
