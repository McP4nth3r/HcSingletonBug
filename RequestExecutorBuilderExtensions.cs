using System.Reflection;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateSingletonBug;

public static class RequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddExtendObjectTypesWithName(
        this IRequestExecutorBuilder builder, string name)
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));

        var assembly = Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes())
        {
            var attributes = type.GetCustomAttributes(typeof(ExtendObjectTypeAttribute), true);
            if (attributes.Length <= 0) continue;
            var attribute = (ExtendObjectTypeAttribute)attributes[0];
            if (attribute.Name == name) builder.AddTypeExtension(type);
        }

        return builder;
    }
}