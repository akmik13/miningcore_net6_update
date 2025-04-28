using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Runtime.Serialization;

namespace Miningcore.Api;

public class StringEnumGenerationProvider : JSchemaGenerationProvider
{
    public override JSchema GetSchema(JSchemaTypeGenerationContext context)
    {
        if (!context.ObjectType.IsEnum)
            return null;

        var schema = new JSchema { Type = JSchemaType.String };
        var enumValues = Enum.GetNames(context.ObjectType);

        foreach (var enumValue in enumValues)
        {
            var enumMember = context.ObjectType.GetMember(enumValue).FirstOrDefault();
            var attr = enumMember?.GetCustomAttribute<EnumMemberAttribute>();

            if (attr != null && !string.IsNullOrEmpty(attr.Value))
                schema.Enum.Add(attr.Value);
            else
                schema.Enum.Add(enumValue);
        }

        return schema;
    }
}

public class JSchemaGenerator : Newtonsoft.Json.Schema.Generation.JSchemaGenerator
{
    // Наследуем класс для дополнительной гибкости реализации
}