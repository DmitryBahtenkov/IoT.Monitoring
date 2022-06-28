using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.InputModel;
using Iot.Main.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers
{
    [ApiController]
    [Route("api/input")]
    public class InputController : ControllerBase
    {
        private const string DeviceKey = "device-token";
		private readonly IConstraintService _constraintService;

		public InputController(IConstraintService constraintService)
		{
			_constraintService = constraintService;
		}

        [HttpPost]
        public async Task ReceiveData([FromBody] InputData data)
        {
            var token = Request.Headers[DeviceKey];

            if(string.IsNullOrEmpty(token))
            {
                return;
            }

            await _constraintService.CheckInputData(token, data);
        }
    }
}