using Iot.Main.Domain.Shared.Helpers;
using Xunit;

namespace Iot.Main.Tests.Domain.Users
{
    public class ClaimsHelperTests
    {
        [Fact]
        public void GetClaimsFromUserTest()
        {
            var user = TestUsers.FirstUser;

            var claims = ClaimsHelper.GetClaimsFromUser(user);

            Assert.Equal(claims.Find(x => x.Type == nameof(user.Login))?.Value, user.Login);
            Assert.Equal(claims.Find(x => x.Type == nameof(user.CompanyId))?.Value, user.CompanyId?.ToString());
            Assert.Equal(claims.Find(x => x.Type == nameof(user.CompanyId))?.Value, user.RoleId.ToString());
        }
    }
}