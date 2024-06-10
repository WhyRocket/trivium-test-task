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

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlite("Data Source=mydatabase.dat");
});

var app = builder.Build();

app.UseHttpLogging();

app.MapGet("/dictionary", (string code, int[] ids, MyDbContext context) =>
{
    var result = default(object);

    if (string.Equals(code, "products", StringComparison.InvariantCultureIgnoreCase))
    {
        result = context.Products
        .Where(product => ids.Contains(product.Id))
        .ToArray();
    }
    else
    {
        result = code;
    }

    return result;
});

ApplyMigrations();

app.Run();

void ApplyMigrations()
{
    var scope = app.Services.CreateScope();

    var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    myDbContext.Database.Migrate();

    scope.Dispose();
}