using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Interfaces;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table("customer")]
public class Customer :  IHasTimestamps
{
    public const int CodeLength = 16;
    private const int NameLength = 150;

    #region Attibutes

    [Key]
    [AttRequired]
    [AttMaxLength(CodeLength)]
    public string Code { get; set; } = string.Empty;

    [AttRequired]
    [AttMaxLength(NameLength)]
    public string Name { get; set; } = string.Empty;

    public bool Active { get; set; } = true;
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }

    #endregion

   
}