using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Models.Attributes;
using BaseProject.Models.Extensions;

namespace BaseProject.Models.Data;

[Table(nameof(Justification))]
public class Justification : TimeStampedModel
{
    #region Attibutes

    [Key] public int Id { get; set; }

    [AttRequired]
    [AttMaxLength(150)]
    public string Name
    {
        get;
        set => field = value.NormalizeText();
    } = string.Empty;

    #endregion
}