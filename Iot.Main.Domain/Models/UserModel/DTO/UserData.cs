using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.UserModel.DTO;

public class UserData : BaseData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Login { get; set; }
    public string Token { get; set; }
    public int? CompanyId { get; set; }
    public string RoleName { get; set; }
}
