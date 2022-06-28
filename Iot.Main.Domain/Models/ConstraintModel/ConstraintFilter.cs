using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.ConstraintModel
{
    public class ConstraintFilter : IFilter<Constraint>
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public Expression<Func<Constraint, bool>> ToExpression()
        {
            if (Id.HasValue)
            {
                return x => x.Id == Id;
            }

            if(!string.IsNullOrEmpty(Name))
            {
                return x => x.Name == Name;
            }

            return IFilter<Constraint>.TrueExpression;
        }
    }
}