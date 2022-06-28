using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.ConstraintModel.DTO;

public class CreateConstraintRequest
{
    public double? MinTemp { get; set; }
    public double? MinHumidity { get; set; }
    public double? MinPressure { get; set; }
    public double? MaxTemp { get; set; }
    public double? MaxHumidity { get; set; }
    public double? MaxPressure { get; set; }
    [Required(ErrorMessage = "Введите название")]
    public string Name { get; set; }
    public int[] AlertIds { get; set; } = Array.Empty<int>();
}
