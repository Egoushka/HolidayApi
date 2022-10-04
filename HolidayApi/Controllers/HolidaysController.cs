using System.Text.Json;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Day;
using HolidayApi.Data.DTO.Holiday;
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
        var cacheData = _cache.TryGetValue<IEnumerable<GetCountryDto>>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = await _holidayService.GetCountries();
        
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
        var cacheData = _cache.TryGetValue<IEnumerable<GetHolidayByYearAndCountryDto>>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = await _holidayService.GetHolidaysByYearAndCountry(request);

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
        var cacheData = _cache.TryGetValue<GetSpecificDayStatusDto>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = await _holidayService.GetSpecificDayStatus(request);

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
        var cacheData = _cache.TryGetValue<GetMaximumNumberOfFreeDaysDto>(cacheKey, out var result);
        if (cacheData)
        {
            return Ok(result);
        }

        result = await _holidayService.GetMaximumNumberOfFreeDays(request);
        
        await _cache.SetAsync(cacheKey, result);
        
        return Ok(result);
    }
}