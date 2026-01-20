using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

public class UserSession
{
    [Key]
    public int Id { get; set; }

    [AttRequired]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [AttRequired]
    [AttMaxLength(255)]
    public string Token { get; set; } = string.Empty;

    public DateTime Expired { get; set; } = DateTime.UtcNow;

    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    
    [AttMaxLength(500)]
    public string? DeviceInfo { get; set; }
}
