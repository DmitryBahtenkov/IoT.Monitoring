using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Tests.Domain.Companies;
using Iot.Main.Tests.Domain.Roles;

namespace Iot.Main.Tests.Domain.Users
{
    public class TestUsers
    {
        public static User FirstUser => new User(
            "f", "f", "f", "f@f.f",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", TestRoles.Role, TestCompanies.CompanyWithRole);

        public static User UserWithoutCompany => new User(
            "f", "f", "f", "f@f.f",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", TestRoles.Role);

        public static User UserWithCompanyAndRoles => new User(
            "f", "f", "f", "f@f.f",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", TestRoles.Role, TestCompanies.CompanyWithRole); 
    }
}