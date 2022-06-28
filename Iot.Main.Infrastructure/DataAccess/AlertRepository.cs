using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Services;

namespace Iot.Main.Infrastructure.DataAccess
{
    public class AlertRepository : BaseRepository<Alert>
    {
        public AlertRepository(IEFContext eFContext, ICurrentUserService currentUserService) : base(eFContext, currentUserService)
        {
        }
    }
}