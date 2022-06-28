using Iot.Main.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers
{
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status500InternalServerError)]
    public class BaseSwaggerController : ControllerBase
    {
        
    }
}