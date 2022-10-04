using HolidayApi.Data;

namespace HolidayApi.Helpers;

public class HolidayHelper
{
    private const int daysInWeek = 7;
    private const int fridayPositionInWeek = 5;
    private const int minimumDifferenceBetweenDays = 1;
    private const int maximumDifferenceBetweenDays = 3;
    public static int GetMaximumNumberOfFreeDays(List<Holiday?> holidays)
    { 
        var result = 0;
        var indexToRemember = 0;
        
        for (int index = 1, offset = 0, tmpIndexToRemember = 0; index < holidays.Count; index++)
        {
            
            var firstDaysCount = holidays[index].Date.ToDaysWithoutYear();
            var secondDaysCount = holidays[tmpIndexToRemember + offset].Date.ToDaysWithoutYear();
            
            var datesDifference = firstDaysCount - secondDaysCount;
            
            if(IsDayWeekend(holidays, tmpIndexToRemember, offset, datesDifference))
            {
                var freeDays = daysInWeek - holidays[tmpIndexToRemember + offset].Date.DayOfWeek;
                
                offset += freeDays;
                datesDifference -= freeDays;
            }
            if(datesDifference == minimumDifferenceBetweenDays)
            {
                offset++;
            }
            else
            {
                if (offset > result)
                {
                    result = offset + 1; // +1 because we need to count first day
                }
                offset = 0;
                tmpIndexToRemember = index;
            }
        }

        return result;
    }

    private static bool IsDayWeekend(List<Holiday> holidays, int startIndex, int offSet, int datesDifference)
    {
        return holidays[startIndex + offSet].Date.DayOfWeek >= fridayPositionInWeek &&
               datesDifference is > minimumDifferenceBetweenDays and <= maximumDifferenceBetweenDays;
    }
}