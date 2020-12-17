using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemTextJsonPolymorphicDeserialization
{
    public class PolymorphicConverter : JsonConverter<BaseModel>
    {
        public readonly Dictionary<string, Type> TypeMapping = new Dictionary<string, Type> {
            { "Bar", typeof(BarModel) },
            { "Foo", typeof(FooModel) }
        };

        public override BaseModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object");
            }
            var typeReader = reader;
            Type? deserializedType = null;
            while(typeReader.Read())
            {
                if(typeReader.TokenType == JsonTokenType.PropertyName && typeReader.GetString() == "Type")
                {
                    if(!typeReader.Read())
                    {
                        throw new JsonException($"Unable to read.");
                    }
                    var typeName = typeReader.GetString();
                    if (typeName is null)
                    {
                        throw new JsonException($"Unable to read the type name.");
                    }
                    if(!this.TypeMapping.TryGetValue(typeName, out deserializedType))
                    {
                        throw new JsonException($"Invalid type in json : {typeName}");
                    }
                    break;
                }
                else if(typeReader.TokenType == JsonTokenType.StartArray || typeReader.TokenType == JsonTokenType.StartObject)
                {
                    typeReader.Skip();
                }
            }

            if(deserializedType == null)
            {
                throw new JsonException($"Key 'Type' not found.");
            }

            return (BaseModel?)JsonSerializer.Deserialize(ref reader, deserializedType, options);
        }

        public override void Write(Utf8JsonWriter writer, BaseModel value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}