using System.Text.Json;
using HolidayApi.Data;
using HolidayApi.Data.Requests;
using HolidayApi.Interfaces;
using HolidayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HolidayApi.Controllers;

public class HolidaysController : Controller
{
    private readonly IHolidayService _holidayService;
    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }
    [HttpGet("holidays")]
    public async Task<IActionResult> GetCountries()
    {
        var result = await _holidayService.GetCountries();
        
        if(result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpGet("holidays/{countryCode}/{year}")]
    public async Task<IActionResult> GetHolidaysByYearAndCountry(string countryCode, int year){
        var request = new GetHolidaysByYearAndCountryRequest
        {
            CountryCode = countryCode,
            Year = year
        };
        
        var result = await _holidayService.GetHolidaysByYearAndCountry(request);
        
        if(result == null)
        {
            return BadRequest();
        }
        return Ok(result);
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