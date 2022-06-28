using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Iot.Main.Tests.Domain.Users
{
    public partial class UserTests
    {
        [Fact]
        public void ToDataTest()
        {
            var user = TestUsers.FirstUser;

            var data = user.ToData();

            Assert.Equal(user.FirstName, data.FirstName);
            Assert.Equal(user.LastName, data.LastName);
            Assert.Equal(user.MiddleName, data.MiddleName);
            Assert.Equal(user.Login, data.Login);
            Assert.Equal(user.Token, data.Token);
            Assert.Equal(user.Role.Name, data.RoleName);
        }
    }
}