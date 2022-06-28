using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
    {
    }
}
