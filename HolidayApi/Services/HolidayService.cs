using AutoMapper;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Holiday;
using HolidayApi.Data.Requests;
using HolidayApi.Interfaces;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace HolidayApi.Services;

public class HolidayService : IHolidayService
{
    private readonly IHttpClientFactory  _httpClientFactory;
    private readonly IMapper  _mapper;
    
    public HolidayService(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCountryDto>?> GetCountries()
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            "?action=getSupportedCountries");
        
        if (!httpResponseMessage.IsSuccessStatusCode) return null;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();


        List<Country> countries = JArray.Parse(json).Select(x => x.ToObject<Country>()).ToList()!;
        return countries.Select(item => _mapper.Map<GetCountryDto>(item));
    }

    public async Task<IEnumerable<IGrouping<int, GetHolidayByYearAndCountryDto>>> GetHolidaysByYearAndCountry(
        GetHolidaysByYearAndCountryRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("getSupportedCountries");
        var httpResponseMessage = await httpClient.GetAsync(
            $"?action=getHolidaysForYear&year={request.Year}&country={request.CountryCode}");
        
        if (!httpResponseMessage.IsSuccessStatusCode) return null;

        var json = await httpResponseMessage.Content.ReadAsStringAsync();


        List<Holiday> countries = JArray.Parse(json).Select(x => x.ToObject<Holiday>()).ToList()!;
        return countries.Select(item => _mapper.Map<GetHolidayByYearAndCountryDto>(item))
            .GroupBy(item => item.Date.Month);
    }
}