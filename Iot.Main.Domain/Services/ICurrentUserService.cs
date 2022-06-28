using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.UserModel;

namespace Iot.Main.Domain.Services
{
    public interface ICurrentUserService
    {
        public User User { get; }
        public int? Userid { get; }
        public int? CompanyId { get; }

        public void Set(User user);
    }
}