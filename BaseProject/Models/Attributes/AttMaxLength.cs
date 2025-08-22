using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models.Attributes;

public class AttMaxLength : MaxLengthAttribute
{
    public AttMaxLength(int length) : base(length)
    {
        ErrorMessage = $@"Debe tener maximo {length} caracteres";
    }
}