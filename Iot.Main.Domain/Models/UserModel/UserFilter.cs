using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.UserModel;

public class UserFilter : IFilter<User>
{
    public int? Id { get; set; }
    public string Fio { get; set; }
    public string Login { get; set; }
    public bool IsAccurateLoginSearch { get; set; }

    public Expression<Func<User, bool>> ToExpression()
    {
        if (Id.HasValue)
        {
            return x => x.Id == Id;
        }

        if (!string.IsNullOrEmpty(Fio))
        {
            return x => Fio.Contains(x.FirstName)
                || Fio.Contains(x.LastName)
                || Fio.Contains(x.MiddleName);
        }

        if (!string.IsNullOrEmpty(Login))
        {
            return IsAccurateLoginSearch 
                ? x => x.Login == Login 
                : x => x.Login.ToLower().Contains(Login.ToLower());
        }

        return IFilter<User>.TrueExpression;
    }
}
