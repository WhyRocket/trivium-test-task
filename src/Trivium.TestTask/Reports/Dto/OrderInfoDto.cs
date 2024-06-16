namespace Reports.Dto;

public class OrderInfoDto
{
    public DictionaryInfoDto Rows { get; set; } = null!;

    public DictionaryInfoDto Columns { get; set; } = null!;
}