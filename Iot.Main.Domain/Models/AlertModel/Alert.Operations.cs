using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.AlertModel.DTO;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Domain.Models.AlertModel;

public partial class Alert
{
    public static Alert Create(AlertRequest request, Company company)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Email, company);

        return new Alert(request.Email, request.Name, company);
    }

    private static void Deduplicate(string email, Company company)
    {
        var existing = company.Alerts.Find(x => x.Email == email);

        if (existing is not null)
        {
            throw new BusinessException($"Данное опопвещение уже существует: {existing.Name}");
        }
    }

    private void Deduplicate(string email)
    {
        Deduplicate(email, Company);
    }

    public void Update(AlertRequest request)
    {
        request.ValidateAndThrow();
        Deduplicate(request.Email);

        Name = request.Name;
        Email = request.Email;
    }
}
