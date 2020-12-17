using System;
using System.Text.Json.Serialization;

namespace SystemTextJsonPolymorphicDeserialization
{
    public class FooModel: BaseModel {
        public string Foo { get; set; } = default!;
    }
}