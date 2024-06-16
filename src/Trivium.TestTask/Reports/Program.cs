var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// HTTP-клиент для обращения к микросервису справочников.
builder.Services.AddHttpClient("DictionariesMicroservice", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["DictionariesMicroserviceUri"]!);
});

var app = builder.Build();

app.MapControllers();

app.Run();