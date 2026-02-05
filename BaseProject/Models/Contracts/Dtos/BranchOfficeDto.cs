using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class BranchOfficeDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public BranchOfficeDto()
    {
    }
    
    public BranchOfficeDto(BranchOffice branchOffice)
    {
        Code = branchOffice.Code;
        Name = branchOffice.Name;
        Country = branchOffice.Country;
    }

    public BranchOffice ToEntity()
    {
        return new BranchOffice
        {
            Code = Code,
            Name = Name,
            Country = Country
        };
    }
}
