using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Interfaces;
using BaseProject.Models.Attributes;
using BaseProject.Models.Extensions;

namespace BaseProject.Models.Data;

[Table("justification")]
public class Justification :  IHasTimestamps
{
    #region Values

    private string _name = string.Empty;

    #endregion

    #region Attibutes

    [Key] public int Id { get; set; }

    [AttRequired]
    [AttMaxLength(150)]
    public string Name
    {
        get => _name;
        set => _name = value.NormalizeText();
    }

    public bool Active { get; set; } = true;
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }

    #endregion

   
}