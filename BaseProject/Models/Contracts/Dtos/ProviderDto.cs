using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class ProviderDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ProviderDto()
    {
    }

    public ProviderDto(Provider provider)
    {
        Code = provider.Code;
        Name = provider.Name;
    }

    public Provider ToEntity()
    {
        return new Provider
        {
            Code = Code,
            Name = Name
        };
    }
}
