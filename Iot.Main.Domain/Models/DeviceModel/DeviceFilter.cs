using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.DeviceModel;

public class DeviceFilter : IFilter<Device>
{
    public int? Id { get; set; }
    public string Token { get; set; }
    public string Name { get; set; }

    public Expression<Func<Device, bool>> ToExpression()
    {
        if (Id.HasValue)
        {
            return x => x.Id == Id;
        }

        if (!string.IsNullOrEmpty(Token))
        {
            return x => x.Token == Token;
        }

        if (!string.IsNullOrEmpty(Name))
        {
            return x => x.Name.ToLower().Contains(Name.ToLower());
        }

        return IFilter<Device>.TrueExpression;
    }
}
