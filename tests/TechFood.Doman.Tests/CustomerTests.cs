using TechFood.Domain.Shared.Exceptions;
using TechFood.Doman.Tests.Fixtures;

namespace TechFood.Doman.Tests;

public class CustomerTests : IClassFixture<CustomerFixture>
{
    private readonly CustomerFixture _customerFixture;
    public CustomerTests(CustomerFixture customerFixture)
    { _customerFixture = customerFixture; }

    [Fact(DisplayName = "Cannot add Customer with invalid document")]
    [Trait("Customer", "Customer Add")]
    public void ShoudThrowException_WhenAddCustomerDocumentCPFInvalid()
    {
        // Act
        var result = Assert.Throws<DomainException>(_customerFixture.CreateInvalidCPFCustomer);
        // Assert
        Assert.Equal(Domain.Resources.Exceptions.Customer_ThrowDocumentCPFInvalid, result.Message);
    }
}
