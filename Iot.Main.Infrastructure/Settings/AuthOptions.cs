using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Iot.Main.Infrastructure.Settings;

public class AuthOptions
{
    public const string Issuer = "IoT";
    public const string Audience = "USER";
    const string Key = "dfkjghcnesriuhcgmildstughmcs;eo ig509ucr5689p8cj5";
    public const int Lifetime = 24 * 60;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
