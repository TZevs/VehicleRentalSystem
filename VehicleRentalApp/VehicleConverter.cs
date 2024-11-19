using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    public class VehicleConverter : JsonConverter<Vehicle>
    {
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;
                string type = root.GetProperty("_TypeOfVehicle").GetString();

                switch (type)
                {
                    case "Car": 
                        return JsonSerializer.Deserialize<Car>(root.GetRawText(), options);
                    case "Van":
                        return JsonSerializer.Deserialize<Van>(root.GetRawText(), options);
                    case "Motorcycle":
                        return JsonSerializer.Deserialize<Motorcycle>(root.GetRawText(), options);
                    default:
                        throw new NotSupportedException($"Vehicle type '{type}' is not supported.");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
