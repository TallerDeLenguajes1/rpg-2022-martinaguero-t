using System.Text.Json.Serialization;
namespace JuegoROL;
public class Localidad
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("dist")]
    public double Dist { get; set; }

    [JsonPropertyName("lid")]
    public int Lid { get; set; }

    [JsonPropertyName("fid")]
    public int Fid { get; set; }

    [JsonPropertyName("int_number")]
    public int IntNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("lat")]
    public string Lat { get; set; }

    [JsonPropertyName("lon")]
    public string Lon { get; set; }

    [JsonPropertyName("zoom")]
    public string Zoom { get; set; }

    [JsonPropertyName("updated")]
    public int Updated { get; set; }

    [JsonPropertyName("weather")]
    public Clima Weather { get; set; }

    [JsonPropertyName("forecast")]
    public object Forecast { get; set; }
}


