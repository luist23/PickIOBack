using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class ProviderDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}
