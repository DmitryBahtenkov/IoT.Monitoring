using Iot.Main.Domain.Aggregates;
using Iot.Main.Domain.Models.UserModel.DTO;

namespace Iot.Main.Domain.Services;

public interface IAuthService
{
    public Task<UserData> Login(LoginRequest request);
    public Task Logout(int id);
    public Task<UserData> Register(RegisterAggregate registerAggregate);
}
