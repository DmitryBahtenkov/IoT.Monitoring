using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Domain.Models.ConstraintModel;

public partial class Constraint
{
    public static Constraint Create(CreateConstraintRequest request, Company company, List<Alert> alerts = null)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Name, company);

        return new Constraint(
            request.Name,
            company,
            request.MinTemp,
            request.MinHumidity,
            request.MinPressure,
            request.MaxTemp,
            request.MaxHumidity,
            request.MaxPressure,
            alerts);
    }

    public void Update(UpdateConstraintRequest request, List<Alert> alerts = null)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Name);

        MinTemp = request.MinTemp;
        MinHumidity = request.MinHumidity;
        MinPressure = request.MinPressure;
        MaxTemp = request.MaxTemp;
        MaxHumidity = request.MaxHumidity;
        MaxPressure = request.MaxPressure;
        Name = request.Name;
        Alerts = alerts ?? new List<Alert>();
    }

    public void CheckInputData(InputData data)
    {
        var errors = new List<string>();

        CheckHumidity(data, errors);
        CheckPressure(data, errors);
        CheckTemp(data, errors);

        if(errors.Any())
        {
            throw new DataAlertException(data, errors);
        }
    }

    private void CheckHumidity(InputData data, List<string> errors)
    {
        if (MaxHumidity.HasValue)
        {
            if (data.Humidity > MaxHumidity)
            {
                errors.Add("Влажность выше нормы");
            }
        }

        if (MinHumidity.HasValue)
        {
            if (data.Humidity < MinHumidity)
            {
                errors.Add("Влажность ниже нормы");
            }
        }
    }

    private void CheckPressure(InputData data, List<string> errors)
    {
        if (MaxPressure.HasValue)
        {
            if (data.Pressure > MaxPressure)
            {
                errors.Add("Давление выше нормы");
            }
        }

        if (MinPressure.HasValue)
        {
            if (data.Pressure < MinPressure)
            {
                errors.Add("Давление ниже нормы");
            }
        }
    }

    private void CheckTemp(InputData data, List<string> errors)
    {
        if (MaxTemp.HasValue)
        {
            if(data.Temp > MaxTemp)
            {
                errors.Add("Температура выше нормы");
            }
        }

        if (MinTemp.HasValue)
        {
            if(data.Temp < MinTemp)
            {
                errors.Add("Температура ниже нормы");
            }
        }
    }

    private static void Deduplicate(string name, Company company)
    {
        if (company.Constraints.Any(x => x.Name == name))
        {
            throw new BusinessException("Такое правило уже существует");
        }
    }

    private void Deduplicate(string name)
    {
        Deduplicate(name, Company);
    }

    public ConstraintData ToData()
    {
        return new ConstraintData
        {
            Id = Id,
            MaxHumidity = MaxHumidity,
            MaxPressure = MaxPressure,
            MaxTemp = MaxTemp,
            MinHumidity = MinHumidity,
            MinPressure = MinPressure,
            MinTemp = MinTemp,
            Name = Name
        };
    }
}
