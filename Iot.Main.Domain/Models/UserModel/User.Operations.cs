using System.Security.Cryptography;
using System.Text;
using Iot.Main.Domain.Extensions;
using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.RoleModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Shared.Exceptions;

namespace Iot.Main.Domain.Models.UserModel;


public partial class User
{
    public static User Create(CreateUserRequest request, Role role, Company company = null)
    {
        request.ValidateAndThrow();
        
        if(company is not null)
        {
            Deduplicate(request.Login, company);
        }

        var (hash, salt) = GeneratePassword(request.Password);

        return new User(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.Login,
            hash,
            salt,
            role,
            company
        );
    }

    private static void Deduplicate(string login, Company company)
    {
        if(company.Users.Any(x => x.Login == login))
        {
            throw new BusinessException("Такой пользователь уже есть в компании");
        }
    }

    public static (string hash, string salt) GeneratePassword(string password)
    {
        var rnd = RandomNumberGenerator.Create();
        var saltBytes = new byte[16];
        rnd.GetBytes(saltBytes);
        var salt = Convert.ToBase64String(saltBytes);

        var sha = SHA512.Create();
        var saltedPass = password + salt;
        var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPass)));
        return (hash, salt);
    }

    public void ChangePassword(ChangePasswordRequest request)
    {
        request.ValidateAndThrow();

        if(!ComparePassword(request.OldPassword))
        {
            throw new BusinessException("Неверный пароль");
        }

        var (hash, salt) = GeneratePassword(request.NewPassword);

        PasswordHash = hash;
        PasswordSalt = salt;
    }

    public static bool ComparePassword(string password, User user)
    {
        var sha = SHA512.Create();
        var saltedPass = password + user?.PasswordSalt;
        var hash = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPass)));

        return hash == user?.PasswordHash;
    }

    public bool ComparePassword(string password)
    {
        return ComparePassword(password, this);
    }

    public void UpdateInfo(UpdateUserRequest request)
    {
        request.ValidateAndThrow();

        if (Company is not null)
        {
            CheckRoleInCompany(request.RoleId);
        }

        FirstName = request.FirstName;
        LastName = request.LastName;
        MiddleName = request.MiddleName;
        RoleId = request.RoleId;
    }

    private void CheckRoleInCompany(int roleId)
    {
        CheckRoleInCompany(Company, roleId);
    }

    private static void CheckRoleInCompany(Company company, int roleId)
    {
        if (!company.Roles.Any(x => x.Id == roleId))
        {
            throw new BusinessException("Неверно указана роль");
        }
    }

    public void Logout()
    {
        Token = null;
    }

    public UserData ToData()
    {
        return new UserData
        {
            Id = Id,
            IsArchive = IsArchive,
            LastName = LastName,
            FirstName = FirstName,
            MiddleName = MiddleName,
            Token = Token,
            CompanyId = CompanyId,
            Login = Login,
            RoleName = Role?.Name ?? RoleId.ToString()
        };
    }
}
