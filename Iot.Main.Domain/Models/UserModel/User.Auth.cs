using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Domain.Shared.Helpers;
using Iot.Main.Domain.Shared.Options;
using Microsoft.IdentityModel.Tokens;

namespace Iot.Main.Domain.Models.UserModel;

public partial class User
{
    public void SetToken(LoginRequest request)
    {
        request.ValidateAndThrow();
        if (!ComparePassword(request.Password))
        {
            throw new BusinessException("Неверный пароль");
        }

        var identity = GetIdentity();

        var jwt = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            notBefore: DateTime.UtcNow,
            claims: identity.Claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        Token = new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private ClaimsIdentity GetIdentity()
    {
        var claims = ClaimsHelper.GetClaimsFromUser(this);

        var claimsIdentity = new ClaimsIdentity(claims, "Token");

        return claimsIdentity;
    }

    public void RemoveToken()
    {
        Token = null;
    }
}
