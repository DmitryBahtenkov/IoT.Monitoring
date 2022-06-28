using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit;

namespace Iot.Main.Tests.Domain.Users
{
    public partial class UserTests
    {
        [Fact]
        public void ValidPasswordLoginTest()
        {
            var request = new LoginRequest()
            {
                Login = "f@f.f",
                Password = "1234"
            };

            var user = TestUsers.FirstUser;
            
            user.SetToken(request);

            Assert.NotEmpty(user.Token);
        }

        [Fact]
        public void InvalidValidPasswordLoginTest()
        {
            var request = new LoginRequest()
            {
                Login = "f@f.f",
                Password = "12345"
            };

            var user = TestUsers.FirstUser;
            
            Assert.Throws<BusinessException>(() => user.SetToken(request));
        }
    }
}