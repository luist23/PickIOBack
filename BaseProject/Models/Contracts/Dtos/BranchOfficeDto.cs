namespace BaseProject.Models.Contracts.Dtos;

public class BranchOfficeDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool Active { get; set; }
}
