using System.Security.Claims;
using Iot.Main.Domain.Models.UserModel;

namespace Iot.Main.Domain.Shared.Helpers;

public static class ClaimsHelper
{
    public static List<Claim> GetClaimsFromUser(User user)
    {
        var props = typeof(User).GetProperties();
        var result = new List<Claim>();

        foreach (var p in props)
        {
            if (p.GetValue(user) is string value)
            {
                result.Add(new Claim(p.Name, value));
            }
        }

        if (user.CompanyId.HasValue)
        {
            result.Add(new Claim(nameof(user.CompanyId), user.CompanyId.ToString()));
        }

        result.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()));
        result.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()));

        return result;
    }
}
