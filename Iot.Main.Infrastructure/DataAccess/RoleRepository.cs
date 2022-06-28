using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
        {
        }
    }
}