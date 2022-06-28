#nullable enable
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;

namespace Iot.Main.Domain.Services;

public interface IUserService
{
    public Task<UserData> Create(CreateUserRequest request);
    public Task<UserData> CreateForCompany(CreateUserRequest request, Company company, Role role);
    public Task<UserData> Update(int id, UpdateUserRequest request);
    public Task<List<UserData>> GetAll(UserFilter filter);
    public Task<UserData?> Get(UserFilter filter);
}
