using AutoMapper;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Holiday;
using HolidayApi.Data.Requests;
using HolidayApi.Interfaces;
using Newtonsoft.Json.Linq;
using HolidayApi.Data.DTO.Day;
using HolidayApi.Data.Response;
using HolidayApi.Helpers;

namespace HolidayApi.Services;

public class HolidayService : IHolidayService
{
    private readonly IHttpClientFactory  _httpClientFactory;
    private readonly IMapper  _mapper;
    private readonly ApplicationContext _context  ;
    
    public HolidayService(IHttpClientFactory httpClientFactory, IMapper mapper, ApplicationContext context)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<GetCountryDto>?> GetCountries()
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            "?action=getSupportedCountries");
        
        if (!httpResponseMessage.IsSuccessStatusCode) return null;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();


        var countries = JArray.Parse(json).Select(x => x.ToObject<Country>()).ToList()!;
        return countries.Select(item => _mapper.Map<GetCountryDto>(item));
    }

    public async Task<IEnumerable<GetHolidayByYearAndCountryDto>> GetHolidaysByYearAndCountry(
        GetHolidaysByYearAndCountryRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            $"?action=getHolidaysForYear&year={request.Year}&country={request.CountryCode}");
        
        if (!httpResponseMessage.IsSuccessStatusCode) return null;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();


        var countries = JArray.Parse(json).Select(x => x.ToObject<Holiday>());
        return countries
            .Select(item => _mapper.Map<GetHolidayByYearAndCountryDto>(item))
            .OrderByDescending(item => item.Date.Month)
            .AsEnumerable();
    }

    public async Task<GetSpecificDayStatusDto> GetSpecificDayStatus(GetSpecificDayStatusRequest request)
    {
        var isDayPublicHoliday = await IsDayPublicHoliday(request);
        var isDayWorkDay = await IsDayWorkDay(request);
        var result = new GetSpecificDayStatusDto
        {
            Status = isDayPublicHoliday ? "public holiday" :  isDayWorkDay? "work day" : "free day"
        };

        return result;
    }
    public async Task<bool> IsDayPublicHoliday(GetSpecificDayStatusRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            $"?action=isPublicHoliday&date={request.Date}&country={request.CountryCode}");
        if (!httpResponseMessage.IsSuccessStatusCode) return false;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();

        var isPublicHolidayResponse = JObject.Parse(json).ToObject<IsPublicHolidayResponse>();

        return isPublicHolidayResponse.IsPublicHoliday;
    }
    public async Task<bool> IsDayWorkDay(GetSpecificDayStatusRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            $"?action=isPublicHoliday&date={request.Date}&country={request.CountryCode}");
        if (!httpResponseMessage.IsSuccessStatusCode) return false;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();

        var isWorkDayResponse = JObject.Parse(json).ToObject<IsWorkDayResponse>();

        return isWorkDayResponse.IsWorkDay;
    }
    
    public async Task<GetMaximumNumberOfFreeDaysDto?> GetMaximumNumberOfFreeDays(GetMaximumNumberOfFreeDaysRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            $"?action=getHolidaysForYear&year={request.Year}&country={request.CountryCode}");
        
        if (!httpResponseMessage.IsSuccessStatusCode) return null;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();

        var holidays = JArray.Parse(json)
            .Select(x => x.ToObject<Holiday>())
            .OrderBy(item => item?.Date.Month)
            .ThenBy(item => item?.Date.Day).ToList();

        var result = HolidayHelper.GetMaximumNumberOfFreeDays(holidays);

        return new GetMaximumNumberOfFreeDaysDto()
        {
            FreeDaysCount = result
        };
    }
}