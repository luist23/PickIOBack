using BaseProject.Models.Attributes;
using BaseProject.Models.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Models.Data;

public class User : IdentityUser
{
    #region Values

    public const int NameLength = 255;
    public const int GuidLength = 36;

    #endregion

    #region Attributes

    [AttRequired]
    [AttMaxLength(NameLength)]
    public string Name
    {
        get;
        set => field = value.NormalizeText();
    } = string.Empty;

    [AttRequired]
    [AttMaxLength(NameLength)]
    public string LastName
    {
        get;
        set => field = value.NormalizeText();
    } = string.Empty;

    public virtual ICollection<UserSession> Sessions { get; set; } = [];

    public bool Active { get; set; } = true;

    #endregion
}