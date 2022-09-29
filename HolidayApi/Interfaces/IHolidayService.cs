using HolidayApi.Data.DTO.Country;

namespace HolidayApi.Interfaces;

public interface IHolidayService
{
    public Task<IEnumerable<GetCountryDto>?> GetCountries();
}