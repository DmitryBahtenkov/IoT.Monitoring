using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot.Main.Domain.Shared.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    { }
}
