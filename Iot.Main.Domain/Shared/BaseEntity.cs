using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iot.Main.Domain.Shared;

public abstract class BaseEntity : IEntityModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsArchive { get; protected set; }

    public void SetArchived(bool isArchive)
    {
        IsArchive = isArchive;
    }
}
