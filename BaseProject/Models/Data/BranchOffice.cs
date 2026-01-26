using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Interfaces;
using BaseProject.Models.Attributes;

namespace BaseProject.Models.Data;

[Table("branch_offices")]
public class BranchOffice :  IHasTimestamps
{
    #region Attributes

    [Key] [AttRequired] [AttMaxLength(5)] public string Code { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(50)] public string Name { set; get; } = string.Empty;
    [AttRequired] [AttMaxLength(2)] public string Country { set; get; } = string.Empty;

    public bool Active { set; get; } = true;
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }

    #endregion

   
}