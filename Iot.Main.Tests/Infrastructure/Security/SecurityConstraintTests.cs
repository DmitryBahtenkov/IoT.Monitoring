using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Units;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Security
{
    public class SecurityConstraintTests : BaseTestCase
    {
		private readonly IRepository<User> _repository;

        private Company _otherCompany;
        private Role _otherRole;
        private List<User> _users;

        public SecurityConstraintTests(
            IRepository<User> repository, 
            IEFContext eFContext, 
            ICurrentUserService currentUserService, 
            ITestOutputHelper outputHelper) : base(eFContext, currentUserService, outputHelper)
        {
            _repository = repository;
        }

        [Fact]
        public async Task GetUsersWithManyCompaniesTest()
        {
            await RunSteps(
                Given_the_company, 
                Given_the_other_company,
                Given_the_admin_role,
                Given_the_other_role,
                Given_the_authenticated_user,
                Given_the_user_in_other_company,
                When_users_has_queried,
                Then_user_in_other_company_not_contains);
        }

        private async Task Given_the_other_company()
        {
            var entry = await _eFContext.Set<Company>().AddAsync(new Company("ttest", "test duplicate"));
            _otherCompany = entry.Entity;
            await _eFContext.SaveChangesAsync();
        }

        private async Task Given_the_other_role()
        {
            var role = new Role("a", _otherCompany);

            var entry = await _eFContext.Set<Role>().AddAsync(role);
            role = entry.Entity;

            _otherCompany.Roles.Add(role);
            _otherRole = role;
        }

        private async Task Given_the_user_in_other_company()
        {
            var user = TestUserInCompany(_otherCompany, _otherRole);
            
            var entry = await _eFContext.Set<User>().AddAsync(user);

            _currentUserService.Set(User);
        }

        private async Task When_users_has_queried()
        {
            _users = await _repository.ListAsync(new UserFilter());
        }

        private Task Then_user_in_other_company_not_contains()
        {
            Assert.DoesNotContain(_users, x => x.FirstName == "ff");

            return Task.CompletedTask;
        }
    }
}