using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Reports.Dto;
using Reports.Extensions;
using System.Data;
using System.Text.Json;

namespace Reports.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Get(OrderInfoDto dto)
    {
        // Метод для получения данных из микросервиса справочников.
        async Task<EntityDto[]> GetDataFromDictionaryMicroserviceAsync(string code, int[] ids)
        {
            var url = $"dictionary?code={code}";

            foreach (var id in ids)
            {
                url += $"&ids={id}";
            }

            var dataString = await _httpClientFactory
                .CreateClient("DictionariesMicroservice")
                .GetStringAsync(url);

            EntityDto[] data;

            // Десериализация полученных данных в зависимости от кода справочника.
            if (code.Equals("products", StringComparison.InvariantCultureIgnoreCase))
            {
                data = JsonSerializer.Deserialize<ProductDto[]>(dataString)!;
            }
            else if (code.Equals("factories", StringComparison.InvariantCultureIgnoreCase))
            {
                data = JsonSerializer.Deserialize<FactoryDto[]>(dataString)!;
            }
            else
            {
                data = [];
            }

            return data;
        }

        var rowsData = await GetDataFromDictionaryMicroserviceAsync(dto.Rows.DictionaryCode, dto.Rows.ElementIds);
        var columnsData = await GetDataFromDictionaryMicroserviceAsync(dto.Columns.DictionaryCode, dto.Columns.ElementIds);

        var order = new DataTable();

        // Создание колонок в таблице.
        // Количество колонок в таблице равно сумме количества атрибутов в строках и количества элементов в колонках.
        for (int i = 0; i < columnsData.Length + dto.Rows.AttributeNames.Length; i++)
        {
            order.Columns.Add();
        }

        // Заполнение заголовков колонок таблицы.
        foreach (var name in dto.Columns.AttributeNames)
        {
            // Заполнение по строкам.
            var row = new string[order.Columns.Count];

            // Заполнение пустых ячеек в строке.
            for (var i = 0; i < dto.Rows.AttributeNames.Length; i++)
            {
                row[i] = string.Empty;
            }

            // Заполнение ячеек заголовками в соответствии с названиями атрибутов.
            for (var i = 0; i < columnsData.Length; i++)
            {
                if (columnsData[i] is FactoryDto factoryData)
                {
                    if (name.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i + dto.Rows.AttributeNames.Length] = factoryData.Name;
                    }
                    if (name.Equals("region", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i + dto.Rows.AttributeNames.Length] = factoryData.Region;
                    }
                    if (name.Equals("year", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i + dto.Rows.AttributeNames.Length] = factoryData.Year.ToString();
                    }
                }
                else if (columnsData[i] is ProductDto productData)
                {

                    if (name.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i + dto.Rows.AttributeNames.Length] = productData.Name;
                    }
                    if (name.Equals("code", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i + dto.Rows.AttributeNames.Length] = productData.Code;
                    }
                }
            }

            order.Rows.Add(row);
        }

        // Заполнение строк.
        foreach (var data in rowsData)
        {
            string[] row = new string[order.Columns.Count];

            // Заполнение строк в соответствии с названиями атрибутов.
            for (var i = 0; i < dto.Rows.AttributeNames.Length; i++)
            {
                if (data is FactoryDto factoryData)
                {
                    if (dto.Rows.AttributeNames[i].Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i] = factoryData.Name;
                    }
                    if (dto.Rows.AttributeNames[i].Equals("region", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i] = factoryData.Region;
                    }
                    if (dto.Rows.AttributeNames[i].Equals("year", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i] = factoryData.Year.ToString();
                    }
                }
                else if (data is ProductDto productData)
                {
                    if (dto.Rows.AttributeNames[i].Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i] = productData.Name;
                    }
                    if (dto.Rows.AttributeNames[i].Equals("code", StringComparison.InvariantCultureIgnoreCase))
                    {
                        row[i] = productData.Code;
                    }
                }
            }

            for (var i = dto.Rows.AttributeNames.Length; i < row.Length; i++)
            {
                row[i] = string.Empty;
            }

            order.Rows.Add(row);
        }

        // Возврат таблицы в HTML-формате.
        return Content(
            content: order.ToHtml(),
            contentType: "text/html");
    }
}
