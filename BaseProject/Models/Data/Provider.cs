using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(Provider))]
public class Provider : TimeStampedModel
{
    public const int CodeLength = 16;
    
    #region Attibutes

    [Key] [AttRequired] [AttMaxLength(CodeLength)] public string Code { get; set; } = string.Empty;
    [AttRequired] [AttMaxLength(150)] public string Name { get; set; } = string.Empty;

    #endregion

   
}