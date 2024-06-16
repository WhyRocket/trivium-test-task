using System.Text.Json.Serialization;

namespace Common.Dto;

public class ProductDto : EntityDto
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = null!;
}
