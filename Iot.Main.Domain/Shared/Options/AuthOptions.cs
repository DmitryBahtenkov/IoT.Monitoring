using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Iot.Main.Domain.Shared.Options;

public class AuthOptions
{
    public static string Issuer => "My application";
    public static string Audience => "My Audience";
    const string Key = "dhfgkdfogjptodrigd59dgoinp609doi6jhpjhod90";
    public const int Lifetime = 24 * 60;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
