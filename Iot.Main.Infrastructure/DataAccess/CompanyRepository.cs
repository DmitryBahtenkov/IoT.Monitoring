using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess;

public class CompanyRepository : BaseRepository<Company>
{
    public CompanyRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
    {
    }
}
