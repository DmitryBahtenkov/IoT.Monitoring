using System;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.CompanyModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Domain.Shared.Exceptions;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Tests.Infrastructure.Base;
using Xunit;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Companies;

public class CompanyTests : BaseTestCase
{
    private readonly ICompanyService _companyService;

    private CompanyData _companyData;
    private CreateCompanyRequest _createCompanyRequest;
    private Exception _exception;

    public CompanyTests(
        ICompanyService companyService,
        IEFContext eFContext,
        ICurrentUserService currentUserService,
        ITestOutputHelper outputHelper) : base(eFContext, currentUserService, outputHelper)
    {
        _companyService = companyService;
    }

    [Fact]
    public async Task CreateValidCompanyTest()
    {
        await RunSteps(
            Given_the_create_request,
            When_company_created,
            Then_company_data_filled,
            Then_company_stored_in_db
        );
    }

    [Fact]
    public async Task CreateDuplicateCompanyTest()
    {
        await RunSteps(
            Given_the_duplicate_company,
            Given_the_duplicate_create_request,
            When_company_created,
            Then_business_exception_was_thrown
        );
    }

    private async Task Given_the_duplicate_company()
    {
        await _eFContext.Set<Company>().AddAsync(new Company("test_duplicate", "test duplicate"));
        await _eFContext.SaveChangesAsync();
    }

    private Task Given_the_create_request()
    {
        _createCompanyRequest = new CreateCompanyRequest
        {
            Name = "test",
            Description = "test testo"
        };

        return Task.CompletedTask;
    }

    private Task Given_the_duplicate_create_request()
    {
        _createCompanyRequest = new CreateCompanyRequest
        {
            Name = "test_duplicate",
            Description = "test testo"
        };

        return Task.CompletedTask;
    }

    private async Task When_company_created()
    {
        await WrapAction(async () => _companyData = await _companyService.Create(_createCompanyRequest));
    }

    private Task Then_company_data_filled()
    {
        Assert.NotNull(_companyData);
        Assert.Equal("test", _companyData.Name);
        Assert.Equal("test testo", _companyData.Description);

        return Task.CompletedTask;
    }

    private async Task Then_company_stored_in_db()
    {
        var company = await _eFContext.Set<Company>().FindAsync(_companyData.Id);

        Assert.NotNull(company);
    }
}
