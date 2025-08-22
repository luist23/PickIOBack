using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BaseProject.Models.Extensions;

public static class ValidationContextExtension
{
    public static string GetDisplayName(this ValidationContext item)
    {
        var displayAttribute = item?.ObjectType
            .GetProperty(item.MemberName ?? "")?
            .GetCustomAttribute<DisplayNameAttribute>();

        return displayAttribute?.DisplayName ?? item?.DisplayName ?? "";
    }
}