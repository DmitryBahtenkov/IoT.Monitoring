using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.DeviceModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Domain.Models.DeviceModel;

public partial class Device
{
    public static Device Create(CreateDeviceRequest request, Company company)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Token, company);
        var constraint = CheckExistConstraint(request.ConstraintId, company);

        return new Device(request.Token, request.Name, constraint, company);
    }

    private static void Deduplicate(string token, Company company)
    {
        if (company.Devices.Any(x => x.Token == token))
        {
            throw new BusinessException("Такое устройство уже существует");
        }
    }

    public void SetState(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    public void SetConstraint(Constraint constraint)
    {
        CheckExistConstraint(constraint.Id);

        Constraint = constraint;
        ConstraintId = constraint.Id;
    }

    private static Constraint CheckExistConstraint(int constraintId, Company company)
    {
        var constraint = company.Constraints.FirstOrDefault(x => x.Id == constraintId);
        if (constraint is null)
        {
            throw new BusinessException("Правило не найдено");
        }

        return constraint;
    }

    private void CheckExistConstraint(int constraintId)
    {
        CheckExistConstraint(constraintId, Company);
    }

    public DeviceData ToData()
    {
        return new DeviceData(Id, Name, IsEnabled, IsArchive);
    }
}
