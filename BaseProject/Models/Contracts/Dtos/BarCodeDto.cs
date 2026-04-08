namespace BaseProject.Models.Contracts.Dtos;

public class BarCodeDto
{
    public string Code { get; set; } = string.Empty;
    public string InternalCode { get; set; } = string.Empty;
    public bool Active { get; set; }
}