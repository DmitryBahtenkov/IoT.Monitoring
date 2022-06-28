using System.Security.Claims;

namespace Iot.Main.Domain.Extensions;

public static class ClaimsIdentityExtensions
{
    public static string GetClaim(this ClaimsPrincipal claimsPrincipal, string key)
    {
        return claimsPrincipal?.Claims.FirstOrDefault(x => x.Type == key)?.Value;
    }

}

