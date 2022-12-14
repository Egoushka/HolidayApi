using System.Reflection;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using HolidayApi.Data;
using HolidayApi.Interfaces;
using HolidayApi.Profiles;
using HolidayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression();
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
builder.Services.AddDistributedPostgreSqlCache(option =>
{
    option.ConnectionString = builder.Configuration.GetConnectionString("DbConnection");
    option.TableName = "cash";
    option.ExpiredItemsDeletionInterval = TimeSpan.FromHours(1);
    option.SchemaName = "public";
});
builder.Services.AddTransient<IHolidayService, HolidayService>();
builder.Services.AddMvc();
builder.Services
    .AddDbContext<ApplicationContext>(optionsBuilder => 
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseResponseCaching();


app.UseAuthorization();

app.MapControllers();

app.Run();