using System.Text.Json.Serialization;

namespace Common.Dto;

public class FactoryDto : EntityDto
{
    [JsonPropertyName("region")]
    public string Region { get; set; } = null!;

    [JsonPropertyName("year")]
    public int Year { get; set; }
}
