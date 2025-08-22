using System.ComponentModel.DataAnnotations;
using BaseProject.Models.Extensions;

namespace BaseProject.Models.Attributes;

public class AttRequiredAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ErrorMessage = $@"El campo {validationContext.GetDisplayName()} es requerido";
        return base.IsValid(value, validationContext);
    }
}