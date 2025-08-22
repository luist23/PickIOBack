namespace BaseProject.Models.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class EnumValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}