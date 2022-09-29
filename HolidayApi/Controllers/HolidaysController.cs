using System.Text.Json;
using HolidayApi.Data;
using HolidayApi.Data.Requests;
using HolidayApi.Interfaces;
using HolidayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HolidayApi.Controllers;
[Route("api/[controller]")]
public class HolidaysController : Controller
{
    private readonly IHolidayService _holidayService;
    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var result = await _holidayService.GetCountries();
        
        if(result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpGet("holidays")]
    public async Task<IActionResult> GetHolidaysByYearAndCountry([FromQuery]string countryCode, [FromQuery]int year){
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
    [HttpGet("getSpecificDay")]
    public async Task<IActionResult> GetSpecificDayStatus([FromQuery]string countryCode, [FromQuery]string date){
        var request = new GetSpecificDayStatusRequest
        {
            CountryCode = countryCode,
            Date = date
        };
        var result = await _holidayService.GetSpecificDayStatus(request);
        return Ok(result);
    }
  
}