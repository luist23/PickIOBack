using Microsoft.AspNetCore.Identity;

namespace BaseProject.Models.Data;

public class Role : IdentityRole
{
    #region Constants
    public static string SuperAdmin => "SuperAdmin";
    public static string Admin => "Admin";
    public static string User => "User";
    #endregion

    #region Attributes
    public int LevelAccess { get; set; }
    #endregion

}