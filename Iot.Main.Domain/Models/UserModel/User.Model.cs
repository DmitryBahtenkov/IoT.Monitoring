using System.ComponentModel.DataAnnotations;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.UserModel;

public partial class User : BaseEntity, IEntityWithNotRequiredCompanyId
{
    public User()
    { }

    public User(
        string firstName, 
        string lastName, 
        string middleName, 
        string login, 
        string passwordHash, 
        string passwordSalt, 
        Role role, 
        Company company = null)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Login = login;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        RoleId = role.Id;
        Role = role;

        if(company is not null)
        {
            CheckRoleInCompany(company, role.Id);
            CompanyId = company?.Id;
            Company = company;
        }

        this.ValidateAndThrow();
    }

    [Required]
    public string FirstName { get; private set; }
    [Required]
    public string LastName { get; private set; }
    public string MiddleName { get; private set; }
    [Required, EmailAddress]
    public string Login { get; private set; }
    [Required]
    public string PasswordHash { get; private set; }
    [Required]
    public string PasswordSalt { get; private set; }
    public string Token { get; private set; }
    public int? CompanyId { get; private set; }
    public virtual Company Company { get; set; }
    [Required]
    public int RoleId { get; private set; }
    public virtual Role Role { get; private set; }
}