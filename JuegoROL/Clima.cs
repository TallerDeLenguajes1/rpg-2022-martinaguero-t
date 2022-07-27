using System.Text.Json.Serialization;
namespace JuegoROL;
public class Clima
{
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("pressure")]
    public double? Pressure { get; set; }

    [JsonPropertyName("st")]
    public double? St { get; set; }

    [JsonPropertyName("visibility")]
    public double Visibility { get; set; }

    [JsonPropertyName("wind_speed")]
    public int? WindSpeed { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("wing_deg")]
    public string WingDeg { get; set; }

    [JsonPropertyName("tempDesc")]
    public string TempDesc { get; set; }
}

