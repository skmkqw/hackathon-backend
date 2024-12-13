namespace HackathonBackend.Domain.Common.Attributes;

public class EfCoreValueConverterAttribute : Attribute
{
    public EfCoreValueConverterAttribute(Type valueConverter)
    {
        ValueConverter = valueConverter;
    }

    public Type ValueConverter { get; }
}