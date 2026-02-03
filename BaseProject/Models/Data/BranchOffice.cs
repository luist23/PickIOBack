using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(BranchOffice))]
public class BranchOffice :  TimeStampedModel
{
    public const int  CodeLength = 5;
    
    #region Attributes

    [Key] [AttRequired] [AttMaxLength(CodeLength)] public string Code { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(50)] public string Name { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(2)] public string Country { set; get; } = string.Empty;

    #endregion
    
}