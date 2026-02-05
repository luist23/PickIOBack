using BaseProject.Models.Data;

namespace BaseProject.Models.Contracts.Dtos;

public class CustomerDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public CustomerDto()
    {
    }

    public CustomerDto(Customer customer)
    {
        Code = customer.Code;
        Name = customer.Name;
    }

    public Customer ToEntity()
    {
        return new Customer
        {
            Code = Code,
            Name = Name
        };
    }
}
