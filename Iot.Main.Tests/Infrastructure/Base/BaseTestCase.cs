using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using Iot.Main.Infrastructure.DataAccess;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.DeviceModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Services;
using Xunit;
using Iot.Main.Domain.Shared.Exceptions;
using Xunit.Abstractions;

namespace Iot.Main.Tests.Infrastructure.Base;

public abstract class BaseTestCase : IDisposable
{
    protected readonly IEFContext _eFContext;
    protected readonly ICurrentUserService _currentUserService;
    private readonly ITestOutputHelper _outputHelper;
    protected Exception Exception { get; set; }

    protected async Task WrapAction(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            Exception = ex;
        }
    }

    protected async Task RunSteps(params Func<Task>[] tasks)
    {
        _outputHelper.WriteLine("-----------");

        var previousName = string.Empty;
        foreach (var task in tasks)
        {
            var methodName = task.Method.Name.Replace('_', ' ');
            if (!string.IsNullOrEmpty(previousName))
            {
                if (previousName.FirstWord() == methodName.FirstWord())
                {
                    methodName = "AND " + methodName;
                }
            }
            _outputHelper.WriteLine(methodName);
            previousName = methodName.Replace("AND ", "");

            await task();
        }
    }

    public BaseTestCase(IEFContext eFContext, ICurrentUserService currentUserService, ITestOutputHelper outputHelper)
    {
        _eFContext = eFContext;
        _currentUserService = currentUserService;
        _outputHelper = outputHelper;
    }

    public void Dispose()
    {
        // var companySet = _eFContext.Set<Company>();
        // var roleSet = _eFContext.Set<Role>();
        // var deviceSet = _eFContext.Set<Device>();
        // var constraintSet = _eFContext.Set<Constraint>();
        // var userSet = _eFContext.Set<User>();

        // foreach (var c in companySet)
        // {
        //     companySet.Remove(c);
        // }

        // foreach (var r in roleSet)
        // {
        //     roleSet.Remove(r);
        // }

        // foreach (var d in deviceSet)
        // {
        //     deviceSet.Remove(d);
        // }

        // foreach (var c in constraintSet)
        // {
        //     constraintSet.Remove(c);
        // }

        // foreach (var u in userSet)
        // {
        //     userSet.Remove(u);
        // }

        // _eFContext.SaveChanges();
    }

    protected Company Company { get; private set; }
    protected Role AdminRole { get; private set; }
    protected User User { get; private set; }

    protected async Task Given_the_company()
    {
        var entry = await _eFContext.Set<Company>().AddAsync(new Company("test_duplicate", "test duplicate"));
        Company = entry.Entity;

        await _eFContext.SaveChangesAsync();
    }

    protected async Task Given_the_admin_role()
    {
        var entry = await _eFContext.Set<Role>().AddAsync(new Role("Admin", Company));
        AdminRole = entry.Entity;

        await _eFContext.SaveChangesAsync();
    }

    protected User TestUser => new User(
            "f", "f", "f", "f@f.f",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", AdminRole, Company);

    protected User TestUserInCompany(Company company, Role role) => new User(
            "ff", "ff", "ff", "ff@ff.ff",
            "n8rFB28xiDbVUCI9Z9pEbEVEpFSv2/g+Rdv55j1t1080N9psA7lza0WIh6K81dGcrClZb4Lb8UsMJqmovPEkxg==",
            "QF6+myCXj717bRsNJJgyDA==", role, company);

    protected async Task Given_the_authenticated_user()
    {
        var user = TestUser;

        var entry = await _eFContext.Set<User>().AddAsync(user);
        User = entry.Entity;

        _currentUserService.Set(User);
    }

    protected Task Then_business_exception_was_thrown()
    {
        Assert.NotNull(Exception);
        Assert.IsType<BusinessException>(Exception);

        return Task.CompletedTask;
    }
}

public static class StringExtensions
{
    public static string FirstWord(this string str)
    {
        return str.Split(' ').FirstOrDefault();
    }
}