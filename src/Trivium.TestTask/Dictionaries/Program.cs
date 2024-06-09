using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Логирование запросов.
builder.Services.AddHttpLogging(optoins =>
{
    optoins.LoggingFields =
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.RequestQuery |
        HttpLoggingFields.RequestMethod |
        HttpLoggingFields.ResponseStatusCode |
        HttpLoggingFields.ResponseBody;
    optoins.CombineLogs = true;
});

var app = builder.Build();

app.UseHttpLogging();

app.Run();