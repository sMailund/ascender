using ascender.Dto;

namespace ascender.Validators;

public class CreateMetricDtoValidator
{
    public bool Validate(CreateMetricDto dto)
    {
        return dto.Window > 0;
    }

}