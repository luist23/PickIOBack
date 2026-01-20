using System.Reflection;

namespace BaseProject.Configuration;

public static class Permissions
{
    public static class Products
    {
        public const string Read = "Permissions.Products.Read";
        public const string Create = "Permissions.Products.Create";
        public const string Edit = "Permissions.Products.Edit";
        public const string Delete = "Permissions.Products.Delete";
    }

    public static class Users
    {
        public const string Read = "Permissions.Users.Read";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }
    
    // Helper to get all permissions via Reflection
    public static List<string> GetAll()
    {
        var permissions = new List<string>();
        foreach (var type in typeof(Permissions).GetNestedTypes())
        {
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
                {
                    permissions.Add((string)field.GetValue(null)!);
                }
            }
        }
        return permissions;
    }
}
