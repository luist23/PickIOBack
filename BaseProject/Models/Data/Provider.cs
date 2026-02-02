using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table("provider")]
public class Provider : TimeStampedModel
{
    #region Attibutes

    [Key] [AttRequired] [AttMaxLength(16)] public string Code { get; set; } = string.Empty;
    [AttRequired] [AttMaxLength(150)] public string Name { get; set; } = string.Empty;

    #endregion

   
}