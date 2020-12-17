using System;
using System.Text.Json.Serialization;

namespace SystemTextJsonPolymorphicDeserialization
{
    public class BarModel: BaseModel {
        public string Bar { get; set; } = default!;
    }
}