using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Enums;

namespace BaseProject.Models.Data;

[Table(nameof(SaleOrderLog))]
public class SaleOrderLog
{
    #region Attibutes

    [Key] public int Id { get; set; }
    public OrderLog TypeLog { get; set; }
    [MaxLength(User.UserIdLength)] [AttRequired] public string UserId { get; set; } = string.Empty;
    public int Date { get; set; }
    public int Time { get; set; }
    public int? InputA { get; set; }
    public int? InputB { get; set; }
    [AttMaxLength(150)] public string? InputC { get; set; }

    #endregion
    
}