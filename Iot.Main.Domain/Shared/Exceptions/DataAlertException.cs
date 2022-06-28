using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.InputModel;

namespace Iot.Main.Domain.Shared.Exceptions;

public class DataAlertException : Exception
{
    public InputData InputData { get; }
    public List<string> Errors { get; }

    public DataAlertException(InputData data, List<string> errors)
    {
        InputData = data;
        Errors = errors;
    }
}
