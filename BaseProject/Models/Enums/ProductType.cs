using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models.Enums;

public enum ProductType
{
    [Display(Name = "Normal")] Default = 0,
    [Display(Name = "Serial")] Serial = 1,
    [Display(Name = "Temporizador")] Timer = 2
}