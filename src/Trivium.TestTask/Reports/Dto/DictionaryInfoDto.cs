namespace Reports.Dto;

public class DictionaryInfoDto
{
    public string DictionaryCode { get; set; } = null!;

    public int[] ElementIds { get; set; } = null!;

    public string[] AttributeNames { get; set; } = null!;
}
