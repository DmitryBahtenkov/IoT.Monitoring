namespace Iot.Main.Domain.Shared;

public interface IEntityModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
