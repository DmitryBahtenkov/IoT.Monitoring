using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iot.Main.Domain.Models.DeviceModel.DTO
{
    public class CreateDeviceRequest
    {
        [Required(ErrorMessage = "Укажите токен")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Укажите название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите правило")]
        public int ConstraintId { get; set; }
    }
}