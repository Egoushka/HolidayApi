using AutoFixture;
using FluentAssertions;
using HolidayApi.Controllers;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Day;
using HolidayApi.Data.DTO.Holiday;
using HolidayApi.Data.Requests;
using HolidayApi.Extensions;
using HolidayApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using DistributedCacheExtensions = HolidayApi.Extensions.DistributedCacheExtensions;

namespace HolidayApiTests;

public class UnitTest1
{
    private readonly Mock<IDistributedCache> _cache;
    private readonly Mock<IDistributedCacheExtensions> _distributedCacheExtensions;
    private readonly Mock<IHolidayService> _holidayService;
    private readonly Fixture _fixture;
    
    public UnitTest1()
    {
        _cache = new Mock<IDistributedCache>();
        _holidayService = new Mock<IHolidayService>();
        _distributedCacheExtensions = new Mock<IDistributedCacheExtensions>();
        _fixture = new Fixture();
    }
    [Fact]
    public async Task GetCountryWithCaching()
    {
        // Arrange
        var countries = GetCountries();
        
        _holidayService.Setup(repo => repo.GetCountries())
            .ReturnsAsync(countries);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, "Countries", out countries))
            .Returns(false);

        _distributedCacheExtensions
            .Setup(extensions => extensions.SetAsync(_cache.Object, "Countries", It.IsAny<byte[]>(), 
                It.IsAny<DistributedCacheEntryOptions>()))
            .Returns(Task.CompletedTask);
        
        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetCountries();
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as List<GetCountryDto>;
        
        // Assert
        result.Should().BeEquivalentTo(GetCountries());
    }
    [Fact]
    public async Task GetCountryWithoutCaching()
    {
        // Arrange
        var countries = GetCountries();
        
        _holidayService.Setup(repo => repo.GetCountries())
            .ReturnsAsync(countries);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, "Countries", out countries))
            .Returns(true);

        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetCountries();
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as List<GetCountryDto>;
        
        // Assert
        result.Should().BeEquivalentTo(GetCountries());
    }
    [Fact]
    public async Task GetHolidaysByYearAndCountryWithCaching()
    {
        // Arrange
        var holidays = GetHolidays();
        _holidayService.Setup(repo => repo.GetHolidaysByYearAndCountry(It.IsAny<GetHolidaysByYearAndCountryRequest>()))
            .ReturnsAsync(holidays);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out holidays))
            .Returns(false);

        _distributedCacheExtensions
            .Setup(extensions => extensions.SetAsync(_cache.Object, It.IsAny<string>(), It.IsAny<byte[]>(), 
                It.IsAny<DistributedCacheEntryOptions>()))
            .Returns(Task.CompletedTask);
        
        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetHolidaysByYearAndCountry("ua", 2022);
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as List<GetHolidayByYearAndCountryDto>;
        
        // Assert
        result.Should().BeEquivalentTo(GetHolidays());
    }
    [Fact]
    public async Task GetHolidaysByYearAndCountryWithoutCaching()
    {
        // Arrange
        var holidays = GetHolidays();
        _holidayService.Setup(repo => repo.GetHolidaysByYearAndCountry(It.IsAny<GetHolidaysByYearAndCountryRequest>()))
            .ReturnsAsync(holidays);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out holidays))
            .Returns(true);

        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetHolidaysByYearAndCountry("ua", 2022);
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as List<GetHolidayByYearAndCountryDto>;
        
        // Assert
        result.Should().BeEquivalentTo(GetHolidays());
    }
    [Fact]
    public async Task GetSpecificDayStatusWithCaching()
    {
        // Arrange
        var specificDayStatus = GetSpecificDayStatus();
        _holidayService.Setup(repo => repo.GetSpecificDayStatus(It.IsAny<GetSpecificDayStatusRequest>()))
            .ReturnsAsync(specificDayStatus);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out specificDayStatus))
            .Returns(false);
        
        _distributedCacheExtensions
            .Setup(extensions => extensions.SetAsync(_cache.Object, It.IsAny<string>(), It.IsAny<byte[]>(), 
                It.IsAny<DistributedCacheEntryOptions>()))
            .Returns(Task.CompletedTask);
        
        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetSpecificDayStatus("ua", "10-10-2022");
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as GetSpecificDayStatusDto;
        
        // Assert
        result.Should().BeEquivalentTo(GetSpecificDayStatus());
    }
    [Fact]
    public async Task GetSpecificDayStatusWithoutCaching()
    {
        // Arrange
        var specificDayStatus = GetSpecificDayStatus();
        _holidayService.Setup(repo => repo.GetSpecificDayStatus(It.IsAny<GetSpecificDayStatusRequest>()))
            .ReturnsAsync(specificDayStatus);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out specificDayStatus))
            .Returns(true);

        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetSpecificDayStatus("ua", "10-10-2022");
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as GetSpecificDayStatusDto;
        
        // Assert
        result.Should().BeEquivalentTo(GetSpecificDayStatus());
    }
    [Fact]
    public async Task GetMaximumFreeDaysWithCaching()
    {
        // Arrange
        var maximumNumberOfFreeDays = GetMaximumFreeDays();
        _holidayService.Setup(repo => repo.GetMaximumNumberOfFreeDays(It.IsAny<GetMaximumNumberOfFreeDaysRequest>()))
            .ReturnsAsync(maximumNumberOfFreeDays);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out maximumNumberOfFreeDays))
            .Returns(false);
        
        _distributedCacheExtensions
            .Setup(extensions => extensions.SetAsync(_cache.Object, It.IsAny<string>(), It.IsAny<byte[]>(), 
                It.IsAny<DistributedCacheEntryOptions>()))
            .Returns(Task.CompletedTask);
        
        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetMaximumFreeDays("ua", 2022);
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as GetMaximumNumberOfFreeDaysDto;
        
        // Assert
        result.Should().BeEquivalentTo(GetMaximumFreeDays());
    }
    [Fact]
    public async Task GetMaximumFreeDaysWithoutCaching()
    {
        // Arrange
        var maximumNumberOfFreeDays = GetMaximumFreeDays();
        _holidayService.Setup(repo => repo.GetMaximumNumberOfFreeDays(It.IsAny<GetMaximumNumberOfFreeDaysRequest>()))
            .ReturnsAsync(maximumNumberOfFreeDays);

        _distributedCacheExtensions
            .Setup(extensions => extensions.TryGetValue(_cache.Object, It.IsAny<string>(), out maximumNumberOfFreeDays))
            .Returns(true);;

        var controller = new HolidaysController(_holidayService.Object, _cache.Object, _distributedCacheExtensions.Object);

        // Act
        var actionResult = await controller.GetMaximumFreeDays("ua", 2022);
        
        var okResult = actionResult as OkObjectResult;
        var result = okResult.Value as GetMaximumNumberOfFreeDaysDto;
        
        // Assert
        result.Should().BeEquivalentTo(GetMaximumFreeDays());
    }
    private IEnumerable<GetCountryDto> GetCountries()
    {
        var result = new List<GetCountryDto>
        {
            new()
            {
                CountryCode = "ua",
                FullName = "Ukraine"
            },
            new()
            {
                CountryCode = "ru",
                FullName = "Russia"
            }
        };

        return result;
    }
    private IEnumerable<GetHolidayByYearAndCountryDto> GetHolidays()
    {
        var result = new List<GetHolidayByYearAndCountryDto>
        {
            new()
            {
                Name = new List<HolidayName>()
                {
                    new()
                    {
                        Lang = "ru",
                        Text = "Новый год"
                    }
                },
                Date = new()
                {
                    Day = 5,
                    Month = 5,
                    Year = 2022,
                    DayOfWeek = 5
                },
                HolidayType = "Holiday"
            }   
        };
      
        return result;
    }
    private GetSpecificDayStatusDto GetSpecificDayStatus()
    {
        var result = new GetSpecificDayStatusDto
        {
            Status = "Free"
        };
      
        return result;
    }
    private GetMaximumNumberOfFreeDaysDto GetMaximumFreeDays()
    {
        var result = new GetMaximumNumberOfFreeDaysDto
        {
            FreeDaysCount = 10
        };
      
        return result;
    }
}