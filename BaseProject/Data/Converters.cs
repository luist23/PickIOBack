using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaseProject.Data;

public class EnumToStringConverter<TEnum>()
    : ValueConverter<TEnum, string>(v => v.ToString(), v => (TEnum)Enum.Parse(typeof(TEnum), v))
    where TEnum : struct, Enum;