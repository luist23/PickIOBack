using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table(nameof(BranchOffice))]
public class BranchOffice :  TimeStampedModel
{
    #region Attributes

    [Key] [AttRequired] [AttMaxLength(5)] public string Code { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(50)] public string Name { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(2)] public string Country { set; get; } = string.Empty;

    #endregion

   
}