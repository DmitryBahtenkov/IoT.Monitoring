using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;
using Iot.Main.Tests.Domain.Companies;
using Iot.Main.Tests.Domain.Roles;

namespace Iot.Main.Tests.Infrastructure.Mocks
{
    public class CurrentUserServiceMock : ICurrentUserService
    {
        public User User { get; private set; }

        public int? Userid { get; private set; }

        public int? CompanyId { get; private set; }

        public void Set(User user)
        {
            User = user;
            Userid = user.Id;
            CompanyId = user.CompanyId ?? 1;
        }
    }
}