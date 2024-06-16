using System.Text.Json.Serialization;

namespace Common.Dto;

public class EntityDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
