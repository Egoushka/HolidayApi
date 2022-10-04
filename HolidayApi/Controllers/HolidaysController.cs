using System.Text.Json;
using HolidayApi.Data;
using HolidayApi.Data.Requests;
using HolidayApi.Extensions;
using HolidayApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace HolidayApi.Controllers;

[Route("api/holiday")]
public class HolidaysController : Controller
{
    private readonly IHolidayService _holidayService;
    private readonly IDistributedCache _cache;

    public HolidaysController(IHolidayService holidayService, IDistributedCache cache)
    {
        _holidayService = holidayService;
        _cache = cache;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var cacheKey = "getAllCountries";
        var cacheData = _cache.TryGetValue<string>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = JsonSerializer.Serialize(await _holidayService.GetCountries(),
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await _cache.SetAsync(cacheKey, result);
        return Ok(result);
    }

    [HttpGet("holidays")]
    public async Task<IActionResult> GetHolidaysByYearAndCountry([FromQuery] string countryCode, [FromQuery] int year)
    {

        var request = new GetHolidaysByYearAndCountryRequest
        {
            CountryCode = countryCode,
            Year = year
        };
        var cacheKey = request.GetHashCode().ToString();
        var cacheData = _cache.TryGetValue<string>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = JsonSerializer.Serialize(await _holidayService.GetHolidaysByYearAndCountry(request),
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await _cache.SetAsync(cacheKey, result);
        
        return Ok(result);

    }

    [HttpGet("specificDayStatus")]
    public async Task<IActionResult> GetSpecificDayStatus([FromQuery] string countryCode, [FromQuery] string date)
    {
        var request = new GetSpecificDayStatusRequest
        {
            CountryCode = countryCode,
            Date = date
        };
        var cacheKey = request.GetHashCode().ToString();
        var cacheData = _cache.TryGetValue<string>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = JsonSerializer.Serialize(await _holidayService.GetSpecificDayStatus(request), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        await _cache.SetAsync(cacheKey, result);
        
        return Ok(result);
    }

    [HttpGet("freeDays")]
    public async Task<IActionResult> GetMaximumFreeDays([FromQuery] string countryCode, [FromQuery] int year)
    {
        var request = new GetMaximumNumberOfFreeDaysRequest
        {
            CountryCode = countryCode,
            Year = year
        };
        var cacheKey = request.GetHashCode().ToString();
        var cacheData = _cache.TryGetValue<string>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = JsonSerializer.Serialize(await _holidayService.GetMaximumNumberOfFreeDays(request),
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        
        await _cache.SetAsync(cacheKey, result);
        
        return Ok(result);
    }
}