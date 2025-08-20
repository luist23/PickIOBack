using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Models.Emuns;
using BaseProject.Models.Extensions;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Models.Data;

public class User : IdentityUser
{
    #region Values
    private string _name = string.Empty;
    private string _lastName = string.Empty;
    #endregion

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

    public Role Role { get; set; } = Role.User;

    public bool Active { get; set; } = true;

}