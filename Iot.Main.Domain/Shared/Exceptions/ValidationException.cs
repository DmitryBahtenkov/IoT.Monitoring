using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Shared.Responses;

namespace Iot.Main.Domain.Shared.Exceptions;

public class ValidationException : Exception
{
    public NotValidEntity Result { get; }
    public ValidationException(Dictionary<string, string> errors)
    {
        Result = new NotValidEntity(errors);
    }

}