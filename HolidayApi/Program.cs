using System.Reflection;
using HolidayApi.Interfaces;
using HolidayApi.Profiles;
using HolidayApi.Services;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "A simple example ASP.NET Core Web API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "My Name",
            Email = string.Empty,
            Url = new Uri("https://example.com/twitter"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under MIT",
            Url = new Uri("https://example.com/license"),
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
});
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(HolidayProfile));
builder.Services.AddHttpClient("getSupportedCountries", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://kayaposoft.com/enrico/json/v2.0/");

    // using Microsoft.Net.Http.Headers;
    // The GitHub API requires two headers.
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, "HttpRequestsSample");
});

builder.Services.AddSingleton<IHolidayService, HolidayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.DocumentTitle = "Todo APIs";
        c.DocExpansion(DocExpansion.None);
        c.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();