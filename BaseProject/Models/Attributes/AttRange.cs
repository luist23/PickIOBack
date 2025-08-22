using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models.Attributes;

public class AttRange : RangeAttribute
{
    public AttRange(int minimum, int maximum) 
        : base(minimum, maximum)
    {
        ErrorMessage = $"El valor debe estar entre {minimum} y {maximum}.";
    }
}