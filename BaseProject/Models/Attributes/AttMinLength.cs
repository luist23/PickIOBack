using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models.Attributes;

public class AttMinLength: MinLengthAttribute
{
    public AttMinLength(int length) : base(length)
    {
        ErrorMessage = $@"Debe tener minimo {length} caracteres";
    }
}