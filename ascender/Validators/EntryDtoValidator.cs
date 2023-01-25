using ascender.Dto;

namespace ascender.Validators;

public class EntryDtoValidator
{
    public bool Validate(EntryDto dto)
    {
        var value = dto.Value.ToString();
        return value.Substring(value.IndexOf(".") + 1).Length <= 2;
    }
}