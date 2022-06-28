using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iot.Main.Domain.Shared.Responses;

public record NotValidEntity(Dictionary<string, string> Errors);
