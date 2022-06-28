using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Tests.Domain.Companies;

namespace Iot.Main.Tests.Domain.Constraints;

public static class TestConstraints
{
    public static Constraint Constraint => new Constraint { Id = 1 };
    public static Constraint ConstraintWithCompany => new Constraint { Id = 1, Company = TestCompanies.CompanyWithDevice };
    public static Constraint FullConstraint => new Constraint
    (
        "test", TestCompanies.CompanyWithDevice, 10, 10, 10, 100, 100, 100
    )
    {
        Id = 1,
    };
}
