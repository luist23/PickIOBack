using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class JustificationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public JustificationDto()
    {
    }

    public JustificationDto(Justification justification)
    {
        Id = justification.Id;
        Name = justification.Name;
    }

    public Justification ToEntity()
    {
        return new Justification
        {
            Id = Id,
            Name = Name
        };
    }
}
