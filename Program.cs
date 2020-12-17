using System;
using System.Text.Json;

namespace SystemTextJsonPolymorphicDeserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = "{\"Type\":\"Foo\",\"Name\":\"Hello\",\"Foo\":\"Foo\"}";
            var input2 = "{\"Name\":\"World\",\"Bar\":\"Bar\",\"Type\":\"Bar\"}";

            var deserialized1 = JsonSerializer.Deserialize<BaseModel>(input1);
            if(deserialized1 is null) {
                Console.WriteLine($"1: null");
            } else {
                Console.WriteLine($"1: {deserialized1.GetType().Name} Name={deserialized1.Name} Foo={(deserialized1 as FooModel)?.Foo}");
            }
            var deserialized2 = JsonSerializer.Deserialize<BaseModel>(input2);
            if(deserialized2 is null) {
                Console.WriteLine($"2: null");
            } else {
                Console.WriteLine($"2: {deserialized2.GetType().Name} Name={deserialized2.Name} Bar={(deserialized2 as BarModel)?.Bar}");
            }
        }
    }
}
