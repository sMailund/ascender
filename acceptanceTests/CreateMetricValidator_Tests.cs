using ascender.Dto;
using ascender.Validators;
using Xunit;

namespace UnitTests.Validators;

public class CreateMetricValidatorTests
{
    private CreateMetricDtoValidator _validator = new();

    [Fact]
    public void ShouldAcceptValidDtoWithWholeNumber()
    {
        var dto = new CreateMetricDto
        {
            Name = 
        }
    }
}