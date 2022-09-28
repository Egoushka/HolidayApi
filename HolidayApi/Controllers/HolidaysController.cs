using System.Text.Json;
using HolidayApi.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HolidayApi.Controllers;

public class HolidaysController : Controller
{
    private readonly IHttpClientFactory  _httpClientFactory;

    public HolidaysController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    [HttpGet("holidays")]
    public async Task<IActionResult> GetCountries(){
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            "?action=getSupportedCountries");
        if (!httpResponseMessage.IsSuccessStatusCode) return BadRequest();

        var json = await httpResponseMessage.Content.ReadAsStringAsync();


        List<Country> countries = JArray.Parse(json).Select(x => x.ToObject<Country>()).ToList()!;
        return Ok(countries.Select(item => item.CountryCode));
    }
    [HttpGet]
    public IActionResult GetHolidaysGroupedByMonthForGivenCountry(){
        return Ok("Hello World");
    }
    [HttpGet]
    public IActionResult GetSpecificDayStatus(){
        return Ok("Hello World");
    }
    [HttpGet]
    public IActionResult GetMaximumNumberOfFreeDays(){
        return Ok("Hello World");
    }
}