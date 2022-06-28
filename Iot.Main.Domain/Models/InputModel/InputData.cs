namespace Iot.Main.Domain.Models.InputModel;

public class InputData
{
    public DateTime Time { get; set; }
    public double? Temp { get; set; }
    public double? Humidity { get; set; }
    public double? Pressure { get; set; }
}
