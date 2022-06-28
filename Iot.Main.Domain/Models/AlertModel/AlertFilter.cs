using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared;

namespace Iot.Main.Domain.Models.AlertModel
{
    public class AlertFilter : IFilter<Alert>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Expression<Func<Alert, bool>> ToExpression()
        {
            if (Id.HasValue)
            {
                return x => x.Id == Id;
            }

            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email))
            {
                return x => x.Email.ToLower().Contains(Email.ToLower()) 
                    && x.Name.ToLower().Contains(Name.ToLower());
            }

            if (!string.IsNullOrEmpty(Email))
            {
                return x => x.Email.ToLower().Contains(Email.ToLower());
            }

            if (!string.IsNullOrEmpty(Name))
            {
                return x => x.Name.ToLower().Contains(Name.ToLower());
            }

            return IFilter<Alert>.TrueExpression;
        }
    }
}