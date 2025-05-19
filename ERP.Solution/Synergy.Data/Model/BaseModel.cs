using System.ComponentModel.DataAnnotations;

namespace Synergy.Data.Model;

public class BaseModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid CreatedBy { get; set; }
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

    public Guid LastUpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}