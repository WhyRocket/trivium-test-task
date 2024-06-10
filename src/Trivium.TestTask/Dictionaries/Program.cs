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
app.MapGet("/dictionary", async (string code, int[] ids, MyDbContext context) =>
{
    var result = default(object);

    if (string.Equals(code, "products", StringComparison.InvariantCultureIgnoreCase))
    {
        result = await context.Products
            .Where(product => ids.Contains(product.Id))
            .ToArrayAsync();
    }

    return result;
});

ApplyMigrations();

app.Run();

// Применение миграций.
void ApplyMigrations()
{
    var scope = app.Services.CreateScope();

    var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    myDbContext.Database.Migrate();

    scope.Dispose();
}