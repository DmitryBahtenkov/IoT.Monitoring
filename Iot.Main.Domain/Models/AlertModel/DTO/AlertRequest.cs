using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iot.Main.Domain.Models.AlertModel.DTO
{
    public record AlertRequest
    {
        [Required(ErrorMessage = "Почта обязательна")]
        public string Email { get; set; }
        [Required(ErrorMessage = "введите название")]
        public string Name { get; set; }
    }
}