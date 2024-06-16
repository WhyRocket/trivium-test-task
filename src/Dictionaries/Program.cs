using Common.Dto;
using Dictionaries.Entities;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Логирование запросов.
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields =
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.RequestQuery |
        HttpLoggingFields.RequestMethod |
        HttpLoggingFields.ResponseStatusCode |
        HttpLoggingFields.ResponseBody;
    options.CombineLogs = true;
});

// Контекст базы данных.
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlite("Data Source=mydatabase.dat");
});

var app = builder.Build();

app.UseHttpLogging();
// GET-метод получения справочника по коду и фильтрацией по id.
app.MapGet("/dictionary", async (string code, int[] ids, MyDbContext context, HttpContext httpContext) =>
{
    var result = default(object);
    // Получение значений из таблицы "Продукция".
    if (string.Equals(code, "products", StringComparison.InvariantCultureIgnoreCase))
    {
        result = await context.Products
            .Where(product => ids.Contains(product.Id) || ids.Length == 0)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code
            })
            .ToArrayAsync();
    }
    // Получение значений из таблицы "Заводы".
    else if (string.Equals(code, "factories", StringComparison.InvariantCultureIgnoreCase))
    {
        result = await context.Factories
            .Where(factory => ids.Contains(factory.Id) || ids.Length == 0)
            .Select(factory => new FactoryDto
            {
                Id = factory.Id,
                Name = factory.Name,
                Region = factory.Region,
                Year = factory.Year
            })
            .ToArrayAsync();
    }
    // Обработка некорректного кода справочника.
    else
    {
        httpContext.Response.StatusCode = 400;
        result = string.Empty;
    }

    return result;
});

ApplyMigrations();

app.Run();

// Применение миграций при запуске приложения.
void ApplyMigrations()
{
    var scope = app.Services.CreateScope();

    var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    myDbContext.Database.Migrate();

    scope.Dispose();
}