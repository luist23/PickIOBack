using System.Linq.Expressions;
using BaseProject.Models.Contracts.Dtos;
using BaseProject.Models.Data;

namespace BaseProject.Models.Mappers;

public static class CustomerMapper
{
    public static Expression<Func<Customer, CustomerDto>> Projection => x => new CustomerDto
    {
        Code = x.Code,
        Name = x.Name
    };

    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto
        {
            Code = customer.Code,
            Name = customer.Name
        };
    }
}
