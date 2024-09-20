using System.Text.Json.Serialization;

namespace SistemaMercado.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UnitType
    {
        Un,   
        G,   
        Kg,
        Ml,
        Lt
    }
}
