using BaseProject.Models.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Models.Data;

public class User : IdentityUser
{
    #region Values
    private string _name = string.Empty;
    private string _lastName = string.Empty;
    #endregion

    #region Attributes
    public string Name
    {
        get => _name;
        set => _name = value.NormalizeText();
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value.NormalizeText();
    }

    public bool Active { get; set; } = true;
    #endregion

}