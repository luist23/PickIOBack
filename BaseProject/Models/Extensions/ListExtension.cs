using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace BaseProject.Models.Extensions;

public static class ListExtension
{
    public static T GetRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            throw new ArgumentException("La lista no puede estar vacía");

        var index = RandomNumberGenerator.GetInt32(list.Count);
        return list[index];
    }
}