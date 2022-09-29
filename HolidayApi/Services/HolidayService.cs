using AutoMapper;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Interfaces;
using Newtonsoft.Json.Linq;

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
}