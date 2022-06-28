using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.CompanyModel;

public class CompanyFilter : IFilter<Company>
{
    public static CompanyFilter WithId(int id)
        => new CompanyFilter 
        {
            Id = id
        };
        
    public int? Id { get; set; }
    public string Name { get; set; }
    public bool IsAccurateSearch { get; set; }

    public Expression<Func<Company, bool>> ToExpression()
    {
        if (Id.HasValue)
        {
            return x => x.Id == Id;
        }

        if(!string.IsNullOrEmpty(Name))
        {
            return IsAccurateSearch 
                ? x => x.Name == Name 
                : x => x.Name.ToLower().Contains(Name.ToLower());
        }

        return IFilter<Company>.TrueExpression;
    }
}
