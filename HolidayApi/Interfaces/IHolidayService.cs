using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Day;
using HolidayApi.Data.DTO.Holiday;
using HolidayApi.Data.Requests;

namespace HolidayApi.Interfaces;

public interface IHolidayService
{
    public Task<IEnumerable<GetCountryDto>> GetCountries();
    public Task<IEnumerable<IGrouping<int, GetHolidayByYearAndCountryDto>>> GetHolidaysByYearAndCountry(
        GetHolidaysByYearAndCountryRequest request);
    public Task<GetSpecificDayStatusDto> GetSpecificDayStatus(GetSpecificDayStatusRequest request);

}