using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Iot.Main.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

        public User User => throw new NotImplementedException();

        public int? Userid 
            => int.TryParse(_httpContextAccessor.HttpContext.User?.Identity?.Name, out var result) ? result : null;

        public int? CompanyId 
            => int.TryParse(_httpContextAccessor.HttpContext.User?.GetClaim(nameof(CompanyId)), out var result) ? result : null;

        public void Set(User user)
        {
            throw new NotImplementedException();
        }
    }
}