using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Constraints
{
    public class ConstraintsServiceTest : BaseTestCase
    {
		private readonly IConstraintService _constraintService;

        private CreateConstraintRequest _request;
        private ConstraintData _data;

        public ConstraintsServiceTest(IEFContext eFContext, ICurrentUserService currentUserService, ITestOutputHelper outputHelper, IConstraintService constraintService) : base(eFContext, currentUserService, outputHelper)
        {
            _constraintService = constraintService;
        }

        [Fact]
        public async Task CreateConstraintTest()
        {
            await RunSteps(
                Given_the_company,
                Given_the_admin_role,
                Given_the_authenticated_user,
                Given_empty_create_request,
                When_constraint_created,
                Then_constraint_name_filled
            );
        }

        private Task Given_empty_create_request()
        {
            _request = new CreateConstraintRequest
            {
                Name = "134"
            };

            return Task.CompletedTask;
        }

        private async Task When_constraint_created()
        {
            _data = await _constraintService.Create(_request); 
        }

        private Task Then_constraint_name_filled()
        {
            Assert.NotNull(_data);
            Assert.Equal(_request.Name, _data.Name);

            return Task.CompletedTask;
        }
    }
}