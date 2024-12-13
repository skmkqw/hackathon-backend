using System.Reflection;
using HackathonBackend.Domain.Common.Attributes;
using HackathonBackend.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackathonBackend.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder AddStronglyTypedIdValueConverters<T>(this ModelBuilder modelBuilder)
    {
        var assembly = typeof(T).Assembly;
        
        foreach (var type in assembly.GetTypes())
        {
            var attribute = type.GetCustomAttribute<EfCoreValueConverterAttribute>();
            if (attribute is null)
                continue;

            var converter = (ValueConverter)Activator.CreateInstance(attribute.ValueConverter)!;

            modelBuilder.UseValueConverterForAggregateRootProperties(type, converter);
        }

        return modelBuilder;
    }

    private static ModelBuilder UseValueConverterForAggregateRootProperties(this ModelBuilder modelBuilder, Type stronglyTypedIdType, ValueConverter converter)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!IsAggregateRootWithStronglyTypedId(entityType.ClrType))
                continue;

            var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == stronglyTypedIdType);
            
            
            foreach (var property in properties)
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converter);
            }
        }

        return modelBuilder;
    }

    private static bool IsAggregateRootWithStronglyTypedId(Type entityType)
    {
        var baseType = entityType;
        while (baseType != null)
        {
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(AggregateRoot<>))
            {
                var idType = baseType.GetGenericArguments()[0];

                return idType.GetCustomAttribute<EfCoreValueConverterAttribute>() != null;
            }
            baseType = baseType.BaseType;
        }
        
        return false;
    }
}