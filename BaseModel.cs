using System;
using System.Text.Json.Serialization;

namespace SystemTextJsonPolymorphicDeserialization
{
    [JsonConverter(typeof(PolymorphicConverter))]
    public abstract class BaseModel {
        public string Name { get; set; } = default!;
    }
}