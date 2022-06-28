using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess
{
    public class ConstraintRepository : BaseRepository<Constraint>
    {
        public ConstraintRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
        {
        }
    }
}