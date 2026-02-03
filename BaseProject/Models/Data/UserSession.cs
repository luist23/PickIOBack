using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(UserSession))]
public class UserSession
{
    #region Constants
    
    public const int TokenLength = 255;

    #endregion

    [Key] public int Id { get; set; }

    [AttRequired]
    [AttMaxLength(User.UserIdLength)]
    public string UserId { get; set; } = string.Empty;

    [AttRequired]
    [AttMaxLength(TokenLength)]
    public string Token { get; set; } = string.Empty;

    public DateTime Expired { get; set; } = DateTime.UtcNow;

    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    [AttMaxLength(500)] public string? DeviceInfo { get; set; }

    #region Relations

    public virtual User? User { get; set; }

    #endregion
}