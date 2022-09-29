using HolidayApi.Data.Requests;
using HolidayApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers;
[Route("api/holiday")]
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
        
        return Ok(result);
    }
    [HttpGet("specificDayStatus")]
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