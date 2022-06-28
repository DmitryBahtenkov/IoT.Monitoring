using System.Linq.Expressions;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.RoleModel;

public class RoleFilter : IFilter<Role>
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public Expression<Func<Role, bool>> ToExpression()
    {
        if (Id.HasValue)
        {
            return x => x.Id == Id;
        }

        if (!string.IsNullOrEmpty(Name))
        {
            return x => x.Name.ToLower().Contains(Name.ToLower());
        }

        return IFilter<Role>.TrueExpression;
    }
}