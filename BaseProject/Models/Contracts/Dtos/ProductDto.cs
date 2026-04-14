using BaseProject.Models.Enums;

namespace BaseProject.Models.Contracts.Dtos;

public class ProductDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool Active { get; set; }
}