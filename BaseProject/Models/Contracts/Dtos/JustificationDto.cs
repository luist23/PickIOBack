using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class JustificationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}
