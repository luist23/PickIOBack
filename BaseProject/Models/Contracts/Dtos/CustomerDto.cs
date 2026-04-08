namespace BaseProject.Models.Contracts.Dtos;

public class CustomerDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}
