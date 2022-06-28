namespace Iot.Main.Domain.Shared;

public interface IEntityWithNotRequiredCompanyId
{
    public int? CompanyId { get; }
}

public interface IEntityWithCompanyId
{
    public int CompanyId { get; }
}